/**************************************************/
/*     Модель таблицы User_Cash из таблицы БД     */
/**************************************************/

using System;


namespace MyBudget.Models
{
    internal class Card_User
    {
        // Id карты
        public int IdCard { get; set; }

        // Id пользователя (члена семьи)
        public int IdUser { get; set; }

        // Номер карты (имя карты)
        public string CardNumber { get; set; }

        // Текущий баланс карты
        public decimal CardBalance { get; set; }
    }
}
