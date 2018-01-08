using DataLogicLayer.Framework;

namespace DataLogicLayer.Models
{
    public class Contact: IEntity
    {
        public int Id { get; set; }
        public  bool Request { get; set; }
        public bool Accept { get; set; }
        public bool IAskedTheQuestion { get; set; }

        public virtual Profile User { get; set; }
        public virtual Profile Friend { get; set; }
    }
}
