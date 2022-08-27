using System;
using System.Runtime.Serialization;

namespace BookmarksStocker.Source.Dtos
{
    [DataContract]
    public class BookmarksDto
    {
        [DataMember]
        public long Id
        { get; set; }

        [DataMember]
        public string Name
        { get; set; }

        [DataMember]
        public string Description
        { get; set; }

        [DataMember]
        public string Url
        { get; set; }

        [DataMember]
        public DateTime CreationTime
        { get; set; }

        [DataMember]
        public DateTime? UpdateTime
        { get; set; }
    }
}