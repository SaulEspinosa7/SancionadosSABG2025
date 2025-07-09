using Microsoft.AspNetCore.Components;

namespace SancionadosSAGB2025.Client.Shared.Forms
{
	partial class MainForm
	{
		[Parameter]
		public bool loading { set; get; } = false;

		[Parameter]
		public RenderFragment? Main { get; set; }

		[Parameter]
		public string? MainHeight { get; set; } = "32vw";

		[Parameter]
		public RenderFragment? ContentBreadcrumbs { set; get; }

		[Parameter]
		public int? view { set; get; } = 2;
	}
}
