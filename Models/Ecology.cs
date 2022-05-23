/**************************************************/
/*       Модель таблицы Ecology из таблицы БД        */
/**************************************************/

namespace MyBudget.Models
{
    class Ecology
    {
        public int IdEcology { get; set; }
        public string DateEcology { get; set; }
        public int IdHouse { get; set; }
        public decimal PriceEcology { get; set; }
        public decimal PayedEcology { get; set; }
        public decimal DebtEcology { get; set; }
    }
}
