using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Tests.Domain.Entities
{
    [TestClass]
    public class AppointmentTests
    {
        private Guid _patientId = Guid.NewGuid();
        private Guid _dentistId = Guid.NewGuid();
        private Guid _dentalOfficeId = Guid.NewGuid();
        private TimeInterval _interval = new TimeInterval(DateTime.UtcNow.AddDays(1),DateTime.UtcNow.AddDays(2));

        [TestMethod]
        public void Constructor_ValidAppointment_StatusIsScheduled()
        {
            var appointment = new Appointment(_patientId,_dentistId,_dentalOfficeId,_interval);

            Assert.AreEqual(_patientId,appointment.PatientId);
            Assert.AreEqual(_dentistId, appointment.DentistId);
            Assert.AreEqual(_dentalOfficeId, appointment.DentalOfficeId);
            Assert.AreEqual(_interval, appointment.TimeInterval);
            Assert.AreEqual(CleanTeeth.Domain.Enums.AppointmentStatus.Scheduled, appointment.Status);
            Assert.AreNotEqual(Guid.Empty, appointment.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_StartTimeInPast_Throws()
        {
            var interval = new TimeInterval(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, interval);
        }

        [TestMethod]
        public void Constructor_CancellingAppointment_ChangesStatusToCancelled()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _interval);
            appointment.Cancel();

            Assert.AreEqual(AppointmentStatus.Canceled,appointment.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Cancel_CancellingAppointment__ThrowsIfStatusIsNotScheduled()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _interval);
            appointment.Cancel();
            appointment.Cancel();
        }

        [TestMethod]
        public void Complete_CompletingAppointment_ChangesStatusToCompleted()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _interval);
            appointment.Complete();
            Assert.AreEqual(AppointmentStatus.Completed,appointment.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Complete_CompletetingAppointment__ThrowsIfStatusIsNotScheduled()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _interval);
            appointment.Cancel();
            appointment.Complete();
        }

    }
}
