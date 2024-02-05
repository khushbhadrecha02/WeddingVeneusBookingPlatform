using System.ComponentModel.DataAnnotations;

namespace WeddingVeneus1.Areas.State.Models
{
    public class StateModel
    {
        [Required]
        public int? StateID { get; set; }
        [Required]
        public string StateName { get; set; }
    }
    public class State_DropDown_Model
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
}
