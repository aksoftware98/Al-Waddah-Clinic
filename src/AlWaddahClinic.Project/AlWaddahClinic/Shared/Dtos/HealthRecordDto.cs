﻿using System;
namespace AlWaddahClinic.Shared.Dtos
{
    public class HealthRecordDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public PatientDto Patient { get; set; }
        public List<NoteDto>? Notes { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}