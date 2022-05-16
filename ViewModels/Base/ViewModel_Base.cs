/************************************
 * БАЗОВЫЙ КЛАСС МОДЕЛИ ПРЕДСТАВЛЕНИЯ
 ***********************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace MyBudget.ViewModels.Base
{
    internal abstract class ViewModel_Base : INotifyPropertyChanged
    {
        /*реализация интерфейса INotifyPropertyChanged*/
        public event PropertyChangedEventHandler PropertyChanged;

        /**********************************************************
         * метод OnPropertyChanged принимает имя свойства и генерирует внутри событие*
         * [CallerMemberName] -> атрибут для компилятора*
         * *********************************************************/
        protected virtual void OnPropertyChanged([CallerMemberName] string PropetyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropetyName));
        }

        /**********************************************************
         * метод Set обновляет значение свойства, для которого определено поле,
         * в котором это свойство хранит данные
         * 'ref T field' -> ссылка на поле свойства
         * 'T value' -> новое значение, которое нужно установить
         * '[CallerMemberName] string PropetyName = null' -> имя свойства (определяется компилятором самостоятельно)
         * '[CallerMemberName] string PropetyName = null' -> передается в метод 'OnPropertyChanged'
         * Метод разрешает кольцевые изменения свойств:
         * когда изменяется свойство 1 система изменяет свойство 2 и т.д.
         **********************************************************/
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropetyName = null)
        {
            if (Equals(field, value))
            {
                return false; // если значение уже соответствует тому, что передали -> возвращаем ложь
            }
            else
            {
                field = value; //если значение изменилось -> обнавляем свойство
                OnPropertyChanged(PropetyName);
                return true;
            }
            
        }

        #region Виртуальные защищённые методы для изменения значений свойств
        /// <summary>Виртуальный метод определяющий изменения в значении поля значения свойства</summary>
        /// <param name="fieldProperty">Ссылка на поле значения свойства</param>
        /// <param name="newValue">Новое значение</param>
        /// <param name="propertyName">Название свойства</param>
        protected virtual void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName] string propertyName = "")
        {
            if ((fieldProperty != null && !fieldProperty.Equals(newValue)) || (fieldProperty == null && newValue != null))
            {
                PropertyNewValue(ref fieldProperty, newValue, propertyName);
            }
        }

        /// <summary>Виртуальный метод изменяющий значение поля значения свойства</summary>
        /// <param name="fieldProperty">Ссылка на поле значения свойства</param>
        /// <param name="newValue">Новое значение</param>
        /// <param name="propertyName">Название свойства</param>
        protected virtual void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            fieldProperty = newValue;
            OnPropertyChanged(propertyName);
        }
        #endregion
    }
}
