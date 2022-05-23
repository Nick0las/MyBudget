/**************************************************/
/*       Модель таблицы Electricity из таблицы БД 
/**************************************************/

namespace MyBudget.Models
{
    class Electricity
    {
        public int IdElectricity { get; set; }
        public int IdHouse { get; set; }
        public string DateElectricity { get; set; }
        public decimal LastMetterElectricity { get; set; }
        public decimal SumWtElectricity { get; set; }
        public decimal PriceElectricity { get; set; }
        public decimal PayedElectricity { get; set; }
        public decimal DebtElectricity { get; set; }
    }
}
