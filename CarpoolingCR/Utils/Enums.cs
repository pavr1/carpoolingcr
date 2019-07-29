using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CarpoolingCR.Utils
{
    public class Enums
    {
        public enum Status
        {
            Activo,
            Inactivo,
            Pendiente,
            Cancelado,
            Bloqueado,
            Lleno,
            Finalizado
        }

        public enum ReservationStatus
        {
            [Description("Aceptado")]
            Accepted,
            [Description("Rechazado")]
            Rejected,
            [Description("Pendiente")]
            Pending,
            [Description("Cancelado")]
            Cancelled
        }

        public enum ProfileStatus
        {
            [Description("Activo")]
            Active,
            [Description("Inactivo")]
            Inactive
        }

        public enum TownStatus
        {
            [Description("Activo")]
            Active,
            [Description("Inactivo")]
            Inactive,
            [Description("Pendiente")]
            Pending
        }

        public enum UserType
        {
            Conductor,
            Pasajero,
            Administrador
        }

        public enum DayPart {
            Mañana,
            Tarde,
            Noche,
            Hora
        }

        public enum EventTriggered
        {
            TripCreated,
            TripDeleted,

        }

        public enum CreditTypes
        {
            [Description("Reservación")]
            Reservation,
            [Description("Depósito")]
            Deposit
        }

        public enum DebitTypes
        {
            [Description("Impuestos y Comisión")]
            Taxes,
            [Description("Reservación")]
            Reservation,
            [Description("Retiro")]
            Withdraw
        }

        public enum LogLocation
        {
            Server,
            Client
        }

        public enum LogType
        {
            Info,
            Error,
            Warning
        }
    }

    public static class EnumHelper<T>
    {
        public static IList<T> GetValues(Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        private static string lookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var result = value.ToString();

            try
            {
                result = fieldInfo.CustomAttributes.ToList()[0].ConstructorArguments[0].Value.ToString();
            }
            catch (Exception)
            {
                //could not find the enum description, display enum value instead. No exception to throw
            }
            
            return result;
        }
    }
}