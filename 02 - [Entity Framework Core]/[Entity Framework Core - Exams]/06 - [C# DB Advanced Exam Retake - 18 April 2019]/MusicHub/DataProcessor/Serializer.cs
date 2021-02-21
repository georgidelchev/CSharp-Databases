using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MusicHub.Data;
using MusicHub.DataProcessor.ExportDtos;
using Newtonsoft.Json;

namespace MusicHub.DataProcessor
{
    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context
                .Albums
                .ToList()
                .Where(a => a.ProducerId == producerId)
                .Select(a => new
                {
                    AlbumName = a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy"),
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs.Select(s => new
                    {
                        SongName = s.Name,
                        Price = s.Price.ToString("f2"),
                        Writer = s.Writer.Name
                    })
                        .ToList()
                        .OrderByDescending(s => s.SongName)
                        .ThenBy(s => s.Writer)
                        .ToList(),
                    AlbumPrice = a.Price.ToString("f2")
                })
                .OrderByDescending(a => a.AlbumPrice)
                .ToList();

            var json = JsonConvert.SerializeObject(albums, Formatting.Indented);

            return json;
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var sb = new StringBuilder();

            var songs = context
                .Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new ExportSongDto()
                {
                    Writer = s.Writer.Name,
                    Performer = s.SongPerformers.Select(sp => sp.Performer.FirstName + " " + sp.Performer.LastName).FirstOrDefault(),
                    SongName = s.Name,
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString(@"hh\:mm\:ss")
                })
                .ToList()
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.Writer)
                .ThenBy(s => s.Performer)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportSongDto>), new XmlRootAttribute("Songs"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, songs, namespaces);
            }

            return sb.ToString().Trim();
        }
    }
}