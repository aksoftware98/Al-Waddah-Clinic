﻿using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Pages.Patients
{
    public partial class ListPatients : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        ApiResponse<IEnumerable<PatientSummaryDto>> patients = new();

        private string _searchText = string.Empty;


        private void GoToAddPatient()
        {
            NavigationManager.NavigateTo("/patients/add");
        }

        protected override async Task OnInitializedAsync()
        {
            patients = await PatientsService.GetAllPatients();
        }

        private void GoToSearchResults()
        {
            if(!string.IsNullOrEmpty(_searchText))
            {
                NavigationManager.NavigateTo($"/patients/search/{_searchText}");
            }
        }
    }
}

