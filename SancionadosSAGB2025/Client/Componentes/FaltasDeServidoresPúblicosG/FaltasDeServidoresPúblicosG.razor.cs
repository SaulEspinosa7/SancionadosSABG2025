using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Componentes.FaltasDeServidoresPúblicosG
{
	partial class FaltasDeServidoresPúblicosG
	{
		private FaltasDeServidoresPublicosG FaltasDeServidoresPublicosG { set; get; } = new();

		private bool panelTwoExpanded = true;
		private bool panelThreeExpanded = false;

		private async Task GuardarDatos() { }

		private void OpenPanelThree()
		{
			panelTwoExpanded = false;
			panelThreeExpanded = true;
		}
	}
}
