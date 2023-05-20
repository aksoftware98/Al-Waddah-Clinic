﻿using System;
using AlWaddahClinic.Server.Data;

namespace AlWaddahClinic.Server.Repositories
{
    public class HealthRecordsRepository : IHealthRecordsRepository
    {
        private readonly ClinicDbContext _context;

        public HealthRecordsRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HealthRecord>> GetHealthRecordsForPatientAsync(int patientId)
        {
            var healthRecords = await _context.HealthRecords.Where(hr => hr.PatientId == patientId).ToListAsync();

            if (!healthRecords.Any())
            {
                throw new NotFoundException("No health records were found for this patient");
            }

            return healthRecords;
        }

        public async Task<HealthRecord> GetHealthRecordByIdAsync(int patientId, int recordId)
        {
            var healthRecord = await _context.HealthRecords.FirstOrDefaultAsync(hr => hr.PatientId == patientId);

            if (healthRecord == null)
            {
                throw new NotFoundException("Health record was not found");
            }

            return healthRecord;
        }

        public async Task AddHealthRecordAsync(int patientId, HealthRecord model)
        {
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient == null)
            {
                throw new NotFoundException("Patient was not found");
            }

            await _context.HealthRecords.AddAsync(model);

            if(model.Notes != null)
            {
                await _context.Notes.AddRangeAsync(model.Notes);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveHealthRecord(int recordId)
        {
            var healthRecord = await _context.HealthRecords.FindAsync(recordId);

            if (healthRecord == null)
            {
                throw new NotFoundException("Health record was not found");
            }

            _context.HealthRecords.Remove(healthRecord);
            await _context.SaveChangesAsync();
        }
    }
}

