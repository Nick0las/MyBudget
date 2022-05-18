/**************************************************/
/*  Модель таблицы water_disposal из таблицы БД   */
/*               'Водоотведение'                  */
/**************************************************/

namespace MyBudget.Models
{
    class WaterRemove
    {
        public int IdWaterRemove { get; set; }
        public int IdHouse { get; set; }
        public string DateWaterRemove { get; set; }
        public decimal KubWaterRemove { get; set; }
        public decimal Price1KubWaterRemove { get; set; }
        public decimal PayedWaterRemove { get; set; }
        public decimal DebtWaterRemove { get; set; }
    }
}
