using System.Runtime.Serialization;

namespace BookmarksStocker.Source.Dtos
{
    [DataContract]
    public class BrowsersDto
    {
        [DataMember]
        public int Id
        { get; set; }

        [DataMember]
        public string Name
        { get; set; }

        [DataMember]
        public string Path
        { get; set; }
    }
}