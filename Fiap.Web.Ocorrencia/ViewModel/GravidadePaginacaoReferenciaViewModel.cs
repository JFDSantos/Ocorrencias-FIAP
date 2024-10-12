namespace Fiap.Web.Ocorrencia.ViewModel
{
    public class GravidadePaginacaoReferenciaViewModel
    {
        public IEnumerable<GravidadeViewModel> Gravidade { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Gravidade?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Gravidade?referencia={NextRef}&tamanho={PageSize}" : "";
    }
}
