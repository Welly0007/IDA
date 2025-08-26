using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CoreLayer.Interfaces;

namespace CoreLayer.Models
{
    [Table("joinUserGroup")]
    public class GroupUser
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Group Group { get; set; }

    }
}
