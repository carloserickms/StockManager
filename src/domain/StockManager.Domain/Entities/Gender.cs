using System.Reflection.Metadata.Ecma335;

namespace domain.StockManager.Domain.Entities
{
    public class Gender : BaseModelId
    {
        public string Name { get; private set; }


        public Gender() : base() { }
        
        public Gender(string name) : base()
        {
            Name = name;
        }
    }
}