﻿using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class HealthRecordCreateDto
	{
		public string Description { get; set; }
		public List<NoteCreateDto>? Notes{ get; set; }
	}
}


