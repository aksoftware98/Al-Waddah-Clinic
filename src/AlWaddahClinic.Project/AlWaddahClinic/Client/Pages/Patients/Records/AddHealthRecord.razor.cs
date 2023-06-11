﻿using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients.Records
{
	public partial class AddHealthRecord : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = null!;

		[Inject]
		public IPatientsService PatientsService { get; set; } = null!;
		[Inject] IHealthRecordsService HealthRecordsService { get; set; } = default!; 
		[Parameter] public Guid PatientId { get; set; }

		private ApiResponse<PatientDto> result = new();
		private PatientDto patient = new();
		private HealthRecordCreateDto model = new();
		private List<NoteCreateDto>? notes = new();

		private string _errorMessage = string.Empty;
		private string _noteTitle = string.Empty;

        protected override async Task OnInitializedAsync()
        {
			result = await PatientsService.GetPatientById(PatientId);
			patient = result.Value;
        }

		//TODO: Implement this method
        private async Task AddRecord()
		{
			_errorMessage = string.Empty;
			try
			{
				model.Notes = notes;
				await HealthRecordsService.AddRecordAsync(PatientId, model);
				NavigationManager.NavigateTo($"/patients/{PatientId}");
			}
			catch (DomainException ex)
			{
				_errorMessage = ex.Message;
			}
		}

		private void AddNote()
		{
			if(!string.IsNullOrEmpty(_noteTitle))
			{
				notes.Add(new NoteCreateDto { Title = _noteTitle });
			}

			_noteTitle = string.Empty;
		}

		private void GoBack()
		{
			NavigationManager.NavigateTo($"/patients/patient/{PatientId}");
		}
	}
}

