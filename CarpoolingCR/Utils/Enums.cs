using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            TripDeleted
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
}