﻿using System;
using System.Security.Cryptography;
using AlWaddahClinic.Shared.Dtos;

namespace AlWaddahClinic.Server.Extensions
{

    public static class DtoMappers
    {
        public static PatientSummaryDto ToPatientSummaryDto(this Patient patient)
        {
            return new PatientSummaryDto
            {
                PatientId = patient.Id,
                FullName = patient.FullName,
                EmailAddress = patient.EmailAddress,
                PhoneNumber = patient.PhoneNumber,
                Gender = patient.Gender
            };
        }

        public static PatientDto ToPatientDto(this Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                EmailAddress = patient.EmailAddress,
                DateOfBirth = patient.DateOfBirth,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                HealthRecords = patient.HealthRecords.Select(h => h.ToHealthRecordSummaryDto()).ToList(),
                Appointments = patient.Appointments.Select(a => a.ToAppointmentSummaryDto()).ToList(),
                Gender = patient.Gender
            };
        }

        public static HealthRecordSummaryDto ToHealthRecordSummaryDto(this HealthRecord healthRecord)
        {
            return new HealthRecordSummaryDto
            {
                Id = healthRecord.Id,
                CreatedOn = (DateTime)healthRecord.CreatedOn
            };
        }

        public static HealthRecordDto ToHealthRecordDto(this HealthRecord healthRecord)
        {
            return new HealthRecordDto
            {
                Id = healthRecord.Id,
                Description = healthRecord.Description,
                CreatedOn = (DateTime)healthRecord.CreatedOn,
                Notes = healthRecord.Notes.Select(n => n.ToNoteDto()).ToList(),
                Patient = healthRecord.Patient.ToPatientDto()
            };
        }

        public static MedicationDto ToMedicationDto(this Medication medication)
        {
            return new MedicationDto
            {
                Id = medication.Id,
                CommercialName = medication.CommercialName,
                DailyDose = medication.DailyDose,
                FinishAt = medication.FinishAt,
                Prescription = medication.Prescription.ToPrescriptionDto()
            };
        }

        public static PrescriptionDto ToPrescriptionDto(this Prescription prescription)
        {
            return new PrescriptionDto
            {
                Id = prescription.Id,
                Description = prescription.Description,
                Medications = prescription.Medications.Select(m => m.ToMedicationDto()).ToList(),
                Appointment = prescription.Appointment.ToAppointmentDto()
            };
        }

        public static AppointmentDto ToAppointmentDto(this Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                Patient = appointment.Patient.ToPatientDto(),
                StartAt = appointment.StartAt,
                FinishAt = appointment.FinishAt,
                Status = appointment.Status
            };
        }

        public static AppointmentSummaryDto ToAppointmentSummaryDto(this Appointment appointment)
        {
            return new AppointmentSummaryDto
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient.FullName,
                StartAt = appointment.StartAt,
                Status = appointment.Status
            };
        }

        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title
            };
        }
    }

    public static class ModelMappers
    {
        public static Clinic ToClinicRegister(this RegisterClinicDto registerClinicDto)
        {
            string password = registerClinicDto.RegisterUser.Password;
            return new Clinic
            {
                Id = Guid.NewGuid(),
                Name = registerClinicDto.Name,
                DoctorName = registerClinicDto.DoctorName,
                DoctorPassword = MapperHelpers.HashPassword(password),
                DoctorEmail = registerClinicDto.DoctorEmail,
                Phone = registerClinicDto.Phone,
                DoctorProfilePicUrl = registerClinicDto.DoctorProfilePicUrl,
                LogoUrl = registerClinicDto.LogoUrl,
                WebsiteUrl = registerClinicDto.WebsiteUrl,
                Area = registerClinicDto.Area,
                StreetAddress = registerClinicDto.StreetAddress,
                City = registerClinicDto.City,
                Country = registerClinicDto.Country,
                StudiedAt = registerClinicDto.StudiedAt,
                GraduatedIn = registerClinicDto.GraduatedIn,
                Specialization = registerClinicDto.Specialization,
                DoctorType = registerClinicDto.DoctorType,
            };
        }

        public static Patient ToPatientCreate(this PatientCreateDto patientDto)
        {
            return new Patient
            {
                FullName = patientDto.FullName,
                EmailAddress = patientDto.EmailAddress,
                PhoneNumber = patientDto.PhoneNumber,
                Address = patientDto.Address,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender,
                Appointments = new List<Appointment>(),
                HealthRecords = new List<HealthRecord>()
            };
        }

        public static Patient ToPatientUpdate(this PatientUpdateDto patientUpdateDto)
        {
            return new Patient
            {
                FullName = patientUpdateDto.FullName,
                EmailAddress = patientUpdateDto.EmailAddress,
                PhoneNumber = patientUpdateDto.PhoneNumber,
                Address = patientUpdateDto.Address,
                DateOfBirth = patientUpdateDto.DateOfBirth,
                Gender = patientUpdateDto.Gender
            };
        }

        public static Patient ToPatient(this PatientDto patientDto)
        {
            return new Patient
            {
                Id = patientDto.Id,
                FullName = patientDto.FullName,
                EmailAddress = patientDto.EmailAddress,
                PhoneNumber = patientDto.PhoneNumber,
                Address = patientDto.Address,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender,
                Appointments = new List<Appointment>(),
                HealthRecords = new List<HealthRecord>()
            };
        }

        public static HealthRecord ToHealthRecordCreate(this HealthRecordCreateDto healthRecordDto)
        {
            return new HealthRecord
            {
                Description = healthRecordDto.Description,
                Notes = healthRecordDto.Notes?.Select(n => n.ToNoteCreate()).ToList()
            };
        }

        public static HealthRecord ToHealthRecord(this HealthRecordDto healthRecordDto)
        {
            return new HealthRecord
            {
                Id = healthRecordDto.Id,
                PatientId = healthRecordDto.Patient.Id,
                Description = healthRecordDto.Description,
                Patient = healthRecordDto.Patient.ToPatient(),
                Notes = healthRecordDto.Notes?.Select(n => n.ToNote()).ToList()
            };

        }

        public static Medication ToMedication(this MedicationDto medicationDto)
        {
            return new Medication
            {
                Id = medicationDto.Id,
                CommercialName = medicationDto.CommercialName,
                DailyDose = medicationDto.DailyDose,
                FinishAt = medicationDto.FinishAt,
                Prescription = medicationDto.Prescription.ToPrescription(),
            };
        }

        public static Prescription ToPrescription(this PrescriptionDto prescriptionDto)
        {
            return new Prescription
            {
                Id = prescriptionDto.Id,
                Description = prescriptionDto.Description,
                Appointment = prescriptionDto.Appointment.ToAppointment(),
                Medications = prescriptionDto.Medications.Select(m => m.ToMedication()).ToList()
            };
        }

        public static Appointment ToAppointment(this AppointmentDto appointmentDto)
        {
            return new Appointment
            {
                Id = appointmentDto.Id,
                PatientId = appointmentDto.Patient.Id,
                Patient = appointmentDto.Patient.ToPatient(),
                StartAt = appointmentDto.StartAt,
                FinishAt = appointmentDto.FinishAt,
            };
        }

        public static Appointment ToAppointmentUpdate(this AppointmentCreateDto appointmentUpdateDto)
        {
            return new Appointment
            {
                StartAt = appointmentUpdateDto.StartAt,
                FinishAt = appointmentUpdateDto.FinishAt
            };
        }

        public static Note ToNoteCreate(this NoteCreateDto noteCreateDto)
        {
            return new Note
            {
                Title = noteCreateDto.Title
            };
        }

        public static Note ToNoteUpdate(this NoteUpdateDto noteUpdateDto)
        {
            return new Note
            {
                Title = noteUpdateDto.Title
            };
        }

        public static Note ToNote(this NoteDto noteDto)
        {
            return new Note
            {
                Id = noteDto.Id,
                Title = noteDto.Title,
                HealthRecord = noteDto.HealthRecord.ToHealthRecord()
            };
        }

    }

    public static class MapperHelpers
    {
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];

            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }

}

