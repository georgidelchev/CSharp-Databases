﻿using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ExportDtos
{
    [XmlType("Song")]
    public class ExportSongDto
    {
        public string SongName { get; set; }

        public string Writer { get; set; }

        public string Performer { get; set; }

        public string AlbumProducer { get; set; }

        public string Duration { get; set; }
    }
}