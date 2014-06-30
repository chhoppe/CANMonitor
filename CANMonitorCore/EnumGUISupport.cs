/* ==============================================================================================================================
 * Manga Viewer Project
 * Author   : Christian Hoppe
 * 
 * All rights reserved.
 * 
 * ==============================================================================================================================
 * Last change on File:
 * 
 * File     : Classes\EnumGUISupport.cs
 * Revision : $LastChangedRevision$
 * Author   : $LastChangedBy$
 * Date     : $LastChangedDate$
 * 
 * Beinhaltet Klassen zur Verwendung von Enums in WPF xamls.
 * Code hauptsächlich von http://www.ageektrapped.com/blog/the-missing-net-7-displaying-enums-in-wpf/
 * 
 * ==============================================================================================================================
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Data;


// TODO Support for Flag Enums

namespace CANMonitor.Core
{
    /// <summary>
    /// Atribute zum beschreiben von Enum Werten
    /// </summary>
    /// <example>Direkte Angabe von Strings für jeden Wert:
    /// <code>public enum MyEnum
    /// {
    ///   [DisplayString("My Value")]
    ///    Value,
    ///   [DisplayString("This means On")]
    ///    On,
    ///   [DisplayString("Off means not On")]
    ///    Off,
    ///   [DisplayString("The great unknown")]
    ///    Unknown,
    ///   [DisplayString("I ain't got none")]
    ///    None
    /// }</code>
    /// </example>
    /// <example>Indirekte Angabe von Strings für jeden Wert über einen ResourceKey:
    /// <code>public enum MyEnum
    /// {
    ///   [DisplayString(ResourceKey="MyEnum_Value")] Value,
    ///   [DisplayString(ResourceKey="MyEnum_On")] On,
    ///   [DisplayString(ResourceKey="MyEnum_Off")] Off,
    ///   [DisplayString(ResourceKey="MyEnum_Unknown")] Unknown,
    ///   [DisplayString(ResourceKey="MyEnum_None")] None
    /// }</code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DisplayStringAttribute : Attribute
    {
        /// <summary>
        /// Speicherort des Strings für den Wert
        /// </summary>
        private readonly string value;
        /// <summary>
        /// Öffentliche Eigenschaft um auf den Wert zuzugreifen
        /// </summary>
        public string Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// Eigenschaft zur Ünterstützung von ResourceKeys
        /// </summary>
        public string ResourceKey
        {
            get;
            set;
        }
        /// <summary>
        /// Konstruktor des Attributes welcher den String zuweist
        /// </summary>
        /// <param name="v">Zuzuweisender String</param>
        public DisplayStringAttribute (string v)
        {
            this.value = v;
        }

        /// <summary>
        /// Konstruktor ohne Zuweisung eines Strings
        /// </summary>
        public DisplayStringAttribute ( )
        {
        }
    }

    /// <summary>
    /// Zuweisung eines Strings zu einem fremden Enum
    /// </summary>
    /// <remarks>
    /// Klasse die eine Beschreibung von Enums ermöglicht die einem nicht gehören und somit es auch nicht möglich ist Ihnen über ein Attribute Strings zuzuweisen.
    /// </remarks>
    /// <example>Direkt im xaml Code kann einem Enum-Wert ein String zugeordnet werden.
    /// <code>
    /// \<sm:EnumDisplayer Type="{x:Type FlickrNet:ContentType}" x:Key="contentTypes"\>
    ///     \<sm:EnumDisplayEntry EnumValue="Photo" DisplayString="Photo (Default)"/\>
    ///     \<sm:EnumDisplayEntry EnumValue="Screenshot" DisplayString="Screenshot"/\>
    ///     \<sm:EnumDisplayEntry EnumValue="Other" DisplayString="Other"/\>
    /// \</sm:EnumDisplayer\>
    /// </code>
    /// </example>
    public class EnumDisplayEntry
    {
        /// <summary>
        /// Der Name des Enum Wertes
        /// </summary>
        public string EnumValue
        {
            get;
            set;
        }
        /// <summary>
        /// Der Angezeigte Name des Wertes
        /// </summary>
        public string DisplayString
        {
            get;
            set;
        }
        /// <summary>
        /// Ausnahme Regelung welche bewirkt das dieser Wert nicht angezeigt wird.
        /// </summary>
        public bool ExcludeFromDisplay
        {
            get;
            set;
        }
    }
    /// <summary>
    /// Klasse zur Anzeige von Enum mit Hilfe von xaml Code.
    /// </summary>
    /// <example>Zunächst muss der Enum in den Resourcen hinzugefügt werden damit auf ihn zugegriffen werden kann.
    /// <code>
    /// \<Application.Resources\>
    ///    \<sm:EnumDisplayer Type="{x:Type FlickrNet:ContentType}" x:Key="contentTypes"\>
    /// \</Application.Resources\>
    /// </code>
    /// Allerdings funktioniert dies nur wenn den Enum-Werten vorher mit Hilfe des DisplayStringAttributes Namen zugeordnet wurden.
    /// Für Enums auf die kein Zugriff besteht und damit es nicht möglich ist Attribute hinzuzufügen bietet die Klasse die möglichkeit
    /// im xaml Code den Werten einen String zuzuordnen:
    /// <code>
    /// \<sm:EnumDisplayer Type="{x:Type FlickrNet:ContentType}" x:Key="contentTypes"\>
    ///     \<sm:EnumDisplayEntry EnumValue="Photo" DisplayString="Photo (Default)"/\>
    ///     \<sm:EnumDisplayEntry EnumValue="Screenshot" DisplayString="Screenshot"/\>
    ///     \<sm:EnumDisplayEntry EnumValue="Other" DisplayString="Other"/\>
    /// \</sm:EnumDisplayer\>
    /// </code>
    /// Damit ist die Resource deklariert aber um ein WPF Element mit den Informationen zu füllen müssen diesem noch die nötigen Bindungen hinzugefügt werden.
    /// Dies geschiet relative einfach wie das Beispiel zeigt.
    /// <code>
    /// \<ComboBox ItemsSource="{Binding Source={StaticResource contentTypes},Path=DisplayNames}"
    ///     SelectedValue="{Binding Path=Batch.Photos/ContentType,
    ///     Converter={StaticResource contentTypes}}" /\>
    /// </code>
    /// </example>
    [System.Windows.Markup.ContentPropertyAttribute("OverriddenDisplayEntries")]
    public class EnumDisplayer : IValueConverter
    {
        /// <summary>
        /// Type des Enums
        /// </summary>
        private Type type;
        /// <summary>
        /// Wörterbuch zur übersetzung von EnumWert nach DisplayString
        /// </summary>
        private IDictionary displayValues;
        /// <summary>
        /// Wörterbuch zur übersetzung von DisplayString nach EnumWert
        /// </summary>
        private IDictionary reverseValues;
        /// <summary>
        /// Liste von überschriebenen Strings, hauptsächlich für fremde Enums
        /// </summary>
        private List<EnumDisplayEntry> overriddenDisplayEntries;

        /// <summary>
        /// Leerer Konstruktor für xaml
        /// </summary>
        public EnumDisplayer ( )
        {
        }

        /// <summary>
        /// Konstruktor der den Enum Type zuweist
        /// </summary>
        /// <param name="type">Enum Type</param>
        public EnumDisplayer (Type type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Öffentliche Eigenschaft zum Zugriff auf den Enum Type
        /// </summary>
        public Type Type
        {
            get
            {
                return type;
            }
            set
            {
                if (!value.IsEnum)
                    throw new ArgumentException("parameter is not an Enumermated type", "value");
                this.type = value;
            }
        }

        /// <summary>
        /// Eigenschaft die eine Liste möglicher Enum-Werte zurückliefert
        /// </summary>
        public ReadOnlyCollection<string> DisplayNames
        {
            get
            {
                // Erstellen des Wörterbuchen für: Enum -> DisplayString
                Type displayValuesType = typeof(Dictionary<,>)
                                            .GetGenericTypeDefinition( ).MakeGenericType(type, typeof(string));
                this.displayValues = (IDictionary)Activator.CreateInstance(displayValuesType);

                // Erstellen des Wörterbuchen für: DisplayString -> Enum
                this.reverseValues =
                   (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>)
                            .GetGenericTypeDefinition( )
                            .MakeGenericType(typeof(string), type));

                // Abrufen alle öffentlichen und statischen Enum-Werte
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
                // Durchlaufen dieser Werte
                foreach (var field in fields)
                {
                    //Abrufen des DisplayStringAttributes
                    DisplayStringAttribute[] a = (DisplayStringAttribute[])
                                                field.GetCustomAttributes(typeof(DisplayStringAttribute), false);

                    // Abfragen des DisplayString Wertes
                    string displayString = GetDisplayStringValue(a);
                    // Erhalten des Enum-Wertes
                    object enumValue = field.GetValue(null);

                    if (displayString == null)
                    {
                        // DisplayStringAttributes war nicht gesetzt, versuch über in Xaml möglicherweise gesetzte Liste
                        displayString = GetBackupDisplayStringValue(enumValue);
                    }
                    if (displayString != null)
                    {
                        // Hinzufügen des Strings und Enum Wertes zu den Wörterbüchern
                        displayValues.Add(enumValue, displayString);
                        reverseValues.Add(displayString, enumValue);
                    }
                }
                // Gibt die Liste der DisplayStrings zurück
                return new List<string>((IEnumerable<string>)displayValues.Values).AsReadOnly( );
            }
        }
        /// <summary>
        /// Erhalten des DisplayStrings aus dem Attribute
        /// </summary>
        /// <param name="a">Attribute</param>
        /// <returns>DisplayString</returns>
        private string GetDisplayStringValue (DisplayStringAttribute[] a)
        {
            if (a == null || a.Length == 0)
                return null;
            DisplayStringAttribute dsa = a[0];
            if (!string.IsNullOrEmpty(dsa.ResourceKey))
            {
                // Wenn ResourceKey gesetzt wurde versuche auf Resource zuzugreifen
                ResourceManager rm = new ResourceManager(type);
                return rm.GetString(dsa.ResourceKey);
            }
            return dsa.Value;
        }
        /// <summary>
        /// Erhalten des DisplayStrings aus der xaml Liste
        /// </summary>
        /// <param name="enumValue">Enum-Wert</param>
        /// <returns>DisplayString</returns>
        private string GetBackupDisplayStringValue (object enumValue)
        {
            if (overriddenDisplayEntries != null && overriddenDisplayEntries.Count > 0)
            {
                // Abfragen ob es einen Eintrag für diesen Wert gibt.
                EnumDisplayEntry foundEntry = overriddenDisplayEntries.Find(delegate(EnumDisplayEntry entry)
                {
                    object e = Enum.Parse(type, entry.EnumValue);
                    return enumValue.Equals(e);
                });
                if (foundEntry != null)
                {
                    // Überprüfen ob dieser Wert nicht dargestellt werden soll
                    if (foundEntry.ExcludeFromDisplay)
                        return null;
                    return foundEntry.DisplayString;

                }
            }
            // Wenn kein Wert angegeben wurde gib Namen des Enum-Wertes zurück
            return Enum.GetName(type, enumValue);
        }
        /// <summary>
        /// Liste von überschriebenen Strings, hauptsächlich für fremde Enums
        /// </summary>
        public List<EnumDisplayEntry> OverriddenDisplayEntries
        {
            get
            {
                if (overriddenDisplayEntries == null)
                    overriddenDisplayEntries = new List<EnumDisplayEntry>( );
                return overriddenDisplayEntries;
            }
        }

        #region IValueConverter
        /// <summary>
        /// Übersetzt von Enum-Wert nach DisplayString
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        object IValueConverter.Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Da Blend ansonsten null reference exception auswirft 
            //und es das anschließende designen erschwert
            if (displayValues == null)
                return null;

            return displayValues[value];
        }
        /// <summary>
        /// Übersetzt von DisplayString nach Enum-Wert 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        object IValueConverter.ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Da Blend ansonsten null reference exception auswirft 
            //und es das anschließende designen erschwert
            if (reverseValues == null)
                return null;

            return reverseValues[value];
        }
        #endregion
    }
}
