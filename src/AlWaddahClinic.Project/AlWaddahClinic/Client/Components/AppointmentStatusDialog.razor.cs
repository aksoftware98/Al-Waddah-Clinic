﻿using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Components
{
    public partial class AppointmentStatusDialog : ComponentBase
    {
        [Inject]
        public IAppointmentsService AppointmentsService { get; set; } = default!;

        [Parameter] public int PatientId { get; set; }
        [Parameter] public int AppointmentId { get; set; }

        private List<NoteCreateDto>? notes = new();

        private string _noteTitle = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isMakingRequest = false;

        private void AddNote()
        {
            if (!string.IsNullOrEmpty(_noteTitle))
            {
                notes.Add(new NoteCreateDto { Title = _noteTitle });
            }

            _noteTitle = string.Empty;
        }


        private async Task Submit()
        {
            _isMakingRequest = true;
            _errorMessage = string.Empty;
            try
            {
                await AppointmentsService.CompleteAppointmentAsync(AppointmentId, model);
            }
            catch(DomainException ex)
            {
                _errorMessage = ex.Message;
            }

            _isMakingRequest = false;
            MudDialog.Close(DialogResult.Ok(true));
        }
        void Cancel() => MudDialog.Cancel();
    }
}

