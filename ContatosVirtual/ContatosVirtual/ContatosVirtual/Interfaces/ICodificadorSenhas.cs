namespace ContatosVirtual.Interfaces
{
    public interface ICodificadorSenhas
    {
        string HashValue(string senhaGerada);
    }
}
