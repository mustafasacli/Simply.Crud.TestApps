using SimpleInfra.Dto.Core;
using System.Runtime.Serialization;

namespace Bookmarks.Project.Dtos
{
    [DataContract]
    public class BrowsersDto : SimpleBaseDto
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