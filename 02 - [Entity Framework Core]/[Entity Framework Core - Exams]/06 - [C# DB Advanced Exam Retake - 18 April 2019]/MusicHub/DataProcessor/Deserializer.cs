using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;
using MusicHub.Data.Models;
using System.Xml.Serialization;
using System.Collections.Generic;
using MusicHub.Data.Models.Enums;
using MusicHub.DataProcessor.ImportDtos;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.DataProcessor
{
    using System;

    using Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportWriterDto>>(jsonString);

            var writersToAdd = new List<Writer>();

            foreach (var writerDto in serializer)
            {
                if (!IsValid(writerDto))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var writer = new Writer()
                {
                    Name = writerDto.Name,
                    Pseudonym = writerDto.Pseudonym
                };

                writersToAdd.Add(writer);

                sb.AppendLine(string.Format(SuccessfullyImportedWriter, writer.Name));
            }

            context.Writers.AddRange(writersToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportProducerDto>>(jsonString);

            var producersToAdd = new List<Producer>();

            foreach (var producerDto in serializer)
            {
                if (!IsValid(producerDto))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var producer = new Producer()
                {
                    Name = producerDto.Name,
                    Pseudonym = producerDto.Pseudonym,
                    PhoneNumber = producerDto.PhoneNumber
                };

                bool flag = false;

                foreach (var albumDto in producerDto.Albums)
                {
                    if (!IsValid(albumDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        flag = true;
                        break;
                    }

                    DateTime releaseDate;

                    var isReleaseDateValid = DateTime.TryParseExact(albumDto.ReleaseDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);

                    if (!isReleaseDateValid)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var album = new Album()
                    {
                        Producer = producer,
                        Name = albumDto.Name,
                        ReleaseDate = releaseDate,
                        ProducerId = producer.Id
                    };

                    producer.Albums.Add(album);
                }

                if (!flag)
                {
                    producersToAdd.Add(producer);

                    if (producer.PhoneNumber == null)
                    {
                        sb.AppendLine(string.Format(SuccessfullyImportedProducerWithNoPhone, producer.Name,
                            producer.Albums.Count));
                    }
                    else
                    {
                        sb.AppendLine(string.Format(SuccessfullyImportedProducerWithPhone, producer.Name,
                            producer.PhoneNumber, producer.Albums.Count));
                    }
                }
            }

            context.Producers.AddRange(producersToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportSongDto>), new XmlRootAttribute("Songs"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            var songsToAdd = new List<Song>();

            using (reader)
            {
                var songDtos = (List<ImportSongDto>)serializer.Deserialize(reader);

                foreach (var songDto in songDtos)
                {
                    if (!IsValid(songDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var album = context.Albums.FirstOrDefault(a => a.Id == songDto.AlbumId);

                    var writer = context.Writers.FirstOrDefault(w => w.Id == songDto.WriterId);

                    var isGenreValid = Enum.IsDefined(typeof(Genre), songDto.Genre);

                    DateTime createdOn;

                    var isCreatedOnValid = DateTime.TryParseExact(songDto.CreatedOn, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out createdOn);

                    if (album == null || writer == null || isGenreValid == false || isCreatedOnValid == false)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var song = new Song()
                    {
                        Name = songDto.Name,
                        Album = album,
                        Writer = writer,
                        AlbumId = songDto.AlbumId,
                        Genre = Enum.Parse<Genre>(songDto.Genre),
                        Price = songDto.Price,
                        WriterId = songDto.WriterId,
                        CreatedOn = createdOn,
                        Duration = TimeSpan.ParseExact(songDto.Duration, @"hh\:mm\:ss", CultureInfo.InvariantCulture)
                    };

                    songsToAdd.Add(song);

                    sb.AppendLine(string.Format(SuccessfullyImportedSong, song.Name, song.Genre, song.Duration));
                }
            }

            context.Songs.AddRange(songsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportPerformerDto>), new XmlRootAttribute("Performers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var performerDtos = (List<ImportPerformerDto>)serializer.Deserialize(reader);

                var performersToAdd = new List<Performer>();

                foreach (var performerDto in performerDtos)
                {
                    if (!IsValid(performerDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var performer = new Performer()
                    {
                        Age = performerDto.Age,
                        FirstName = performerDto.FirstName,
                        LastName = performerDto.LastName,
                        NetWorth = performerDto.NetWorth
                    };

                    bool flag = false;

                    foreach (var songDto in performerDto.PerformerSongs)
                    {
                        if (!IsValid(songDto))
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        var song = context.Songs.FirstOrDefault(s => s.Id == songDto.Id);

                        if (song == null)
                        {
                            sb.AppendLine(ErrorMessage);

                            flag = true;

                            break;
                        }

                        performer.PerformerSongs.Add(new SongPerformer()
                        {
                            Performer = performer,
                            Song = song
                        });
                    }

                    if (!flag)
                    {
                        performersToAdd.Add(performer);

                        sb.AppendLine(string.Format(SuccessfullyImportedPerformer, performer.FirstName,
                            performer.PerformerSongs.Count));
                    }
                }

                context.Performers.AddRange(performersToAdd);

                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}