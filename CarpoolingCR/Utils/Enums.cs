using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CarpoolingCR.Utils
{
    public class Enums
    {
        public enum Status
        {
            Activo = 1,
            Inactivo = 2,
            Pendiente = 3,
            Cancelado = 4,
            Bloqueado = 5,
            Lleno = 6,
            Finalizado = 7
        }

        public enum ReservationStatus
        {
            [Description("Aceptado")]
            Accepted = 1,
            [Description("Rechazado")]
            Rejected = 2,
            [Description("Pendiente")]
            Pending = 3,
            [Description("Cancelado")]
            Cancelled = 4,
            [Description("Finalizado")]
            Finalized = 5
        }

        public enum ProfileStatus
        {
            [Description("Activo")]
            Active = 1,
            [Description("Inactivo")]
            Inactive = 2
        }

        public enum TownStatus
        {
            [Description("Activo")]
            Active = 1,
            [Description("Inactivo")]
            Inactive = 2,
            [Description("Pendiente")]
            Pending = 3
        }

        public enum UserType
        {
            Conductor = 0,
            Pasajero = 1,
            Administrador = 2
        }

        public enum DayPart
        {
            Mañana = 1,
            Tarde = 2,
            Noche = 3,
            Hora = 4
        }

        public enum EventTriggered
        {
            TripCreated = 1,
            TripDeleted = 2,

        }

        public enum CreditTypes
        {
            [Description("Reservación")]
            Reservation = 1,
            [Description("Depósito")]
            Deposit = 2
        }

        public enum DebitTypes
        {
            [Description("Impuestos y Comisión")]
            Taxes = 1,
            [Description("Reservación")]
            Reservation = 2,
            [Description("Retiro")]
            Withdraw = 3
        }

        public enum LogLocation
        {
            Server = 1,
            Client = 2
        }

        public enum LogType
        {
            Info = 1,
            Error = 2,
            Warning = 3,
            SMS = 4,
            UserIdVerification = 5,
            VehicleCreation = 6
        }

        public enum RequestNotificationStatus
        {
            [Description("Activo")]
            Active = 1,
            [Description("Cancelado")]
            Cancelled = 2,
            [Description("Finalizado por reserva")]
            Finished = 3,
            [Description("Expirado")]
            Expired = 4
        }

        public enum EmailType
        {
            Notifications = 1,
            Updates = 2,
            Errors = 3
        }

        public enum PromoStatus
        {
            Active = 1,
            Inactive = 2
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