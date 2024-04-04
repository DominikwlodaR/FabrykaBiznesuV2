using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabrykaBiznesuV2.Models
{
    public class AgendaComments
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName {  get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public string Comment {  get; set; }    
        public List<AgendaComments> Replies { get; set; }
        public string Date {  get; set; }
        public bool isReply { get; set; }
        public int? ParentId{ get; set; }


        public Project? Project { get; set; }
    }
}
