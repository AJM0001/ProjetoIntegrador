
namespace MvpPesquisador.Negocio
{
    
public abstract class NegocioBase<TNegocio>
where TNegocio : class
    {
        private static TNegocio instancia;
        public static TNegocio Instancia { get { return instancia = instancia ?? Activator.CreateInstance<TNegocio>(); } }
    }
}

