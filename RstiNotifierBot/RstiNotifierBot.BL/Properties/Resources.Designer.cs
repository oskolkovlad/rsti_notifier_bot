﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RstiNotifierBot.BL.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RstiNotifierBot.BL.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на *КЛИЕНТСКИЙ ОТДЕЛ*:
        ///`Тел.:` (812) 321-92-08, 318-17-38, 964-63-03
        ///
        ///`Обед:` с 13:00 до 14:00
        ///
        ///*График работы:*
        ///`Пн:` с 9:00 до 19:00
        ///`Вт-Чт:` с 9:00 до 18:00
        ///`Пт:` с 9:00 до 17:00
        ///`Сб:` с 10:00 до 17:00
        ///
        ///При подписании дополнительных документов по договорам долевого участия в субботу необходимо предварительно записаться в клиентском отделе по телефону..
        /// </summary>
        internal static string Contacts {
            get {
                return ResourceManager.GetString("Contacts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на https://instagram.com/rsti.group?utm_medium=copy_link.
        /// </summary>
        internal static string InstagramUrl {
            get {
                return ResourceManager.GetString("InstagramUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на https://rsti.ru/news.
        /// </summary>
        internal static string NewsUrl {
            get {
                return ResourceManager.GetString("NewsUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Subscription.
        /// </summary>
        internal static string SubscriptionPropertyName {
            get {
                return ResourceManager.GetString("SubscriptionPropertyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на https://www.youtube.com/channel/UCXEyCUWTTWY7XXTJPkhS8SA/videos.
        /// </summary>
        internal static string YoutubeChannelUrl {
            get {
                return ResourceManager.GetString("YoutubeChannelUrl", resourceCulture);
            }
        }
    }
}
