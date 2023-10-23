using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Sistema
    {
        #region Properties
        //================PROPERTIES================//
        private List<Actividad> ListaActividades { get; }
        private List<Proveedor> ListaProveedores { get; }
        private List<Usuario> ListaUsuarios { get; }        
        private List<Agenda> ListaAgendas { get; }
        #endregion

        #region Singleton
        //============INICIALIZADOR DE SISTEMA============//
        private static Sistema ?instanciaSistema;

        public static Sistema ObtenerInstancia
        {
            get
            {
                if (instanciaSistema == null) instanciaSistema = new Sistema();
                return instanciaSistema;
            }
        }

        private Sistema()
        {
            ListaActividades = new List<Actividad>();
            ListaProveedores = new List<Proveedor>();
            ListaUsuarios = new List<Usuario>();
            ListaAgendas = new List<Agenda>();            
            Precarga();
        }

        #endregion

        //================METODOS================//

        //-----------------PRECARGA()-----------------//
        private void Precarga()
        {
            //------usuarios------//
            Operador op1 = new Operador("operador@gmail.com", "12345678", "Rodolfo", "Gutierrez", "OPERADOR", new DateTime(1967, 03, 18));
            Operador op2 = new Operador("operador2@gmail.com", "12345678", "Andres", "Acosta", "OPERADOR", new DateTime(1984, 10, 07));            
            Huesped huesped = new Huesped("huesped@gmail.com", "12345678", TipoDocumento.CI, "59001144", "Andrea", "Vidal", "405", new DateTime(1997, 03, 12));
            huesped.Rol = "HUESPED";
            huesped.Fidelizacion = 4; //Lo dejamos asi para que se pueda corroborar que los calculos son correctos

            //------proveedor------//
            Proveedor DreamWorks = new Proveedor("DreamWorks S.R.L.", "23048549", "Suarez 3380 Apto 304", 10);
            Proveedor EstelaUmp = new Proveedor("Estela Umpierrez S.A.", "33459678", "Lima 2456", 7);
            Proveedor TravelFun = new Proveedor("TravelFun", "29152020", "Misiones 1140", 9);
            Proveedor Rekreation = new Proveedor("Rekreation S.A.", "29162019", "Bacacay 1211", 11);
            Proveedor AlonsoUmp = new Proveedor("Alonso & Umpierrez", "24051920", "18 de Julio 1956 Apto 4", 10);
            Proveedor ElectricBlue = new Proveedor("Electric Blue", "26018945", "Cooper 678", 5);
            Proveedor Ludica = new Proveedor("Lúdica S.A.", "26142967", "Dublin 560", 4);
            Proveedor Gimenez = new Proveedor("Gimenez S.R.L.", "29001010", "Andes 1190", 7);
            Proveedor ProvX = new Proveedor("", "22041120", "Agraciada 2512 Apto. 1", 8);
            Proveedor NorbertoMol = new Proveedor("Norberto Molina", "22001189", "Paraguay 2100", 9);

            //------actividades-hostal------//
            Actividad Senderismo = new ActividadHostal("Senderismo", "Disfrute de una caminata guiada por las hermosas montañas locales", new DateTime(2025, 7, 15, 10, 0, 0), 10, 12, "Juan Perez", "Montañas del hostal", true, 20);
            Actividad Yoga = new ActividadHostal("Yoga matutino", "Despierta con una relajante sesión de yoga en la terraza", new DateTime(2025, 7, 11, 23, 0, 0), 1, 16, "Maria Rodriguez", "Terraza del hostal", true, 10);
            Actividad Cine = new ActividadHostal("Noche de cine", "Disfruta de una película al aire libre en la zona de jardín del hostal", new DateTime(2025, 7, 17, 20, 30, 0), 20, 18, "Ana Garcia", "Jardín del hostal", true, 5);
            Actividad TourGastronomico = new ActividadHostal("Tour gastronómico", "Explora los sabores locales en una divertida caminata por la ciudad", new DateTime(2025, 7, 18, 12, 0, 0), 8, 21, "Pablo Martinez", "Ciudad del hostal", true, 30);
            Actividad TallerArtesania = new ActividadHostal("Taller de artesanía", "Aprende a hacer artesanías locales con artesanos de la zona", new DateTime(2025, 7, 19, 14, 0, 0), 12, 10, "Luisa Gomez", "Taller de artesanos del hostal", false);
            Actividad ExcursionBicicleta = new ActividadHostal("Excursión en bicicleta", "Pasea en bicicleta por los hermosos campos cercanos", new DateTime(2025, 7, 16, 23, 0, 0), 6, 7, "Diego Torres", "Campos cercanos al hostal", true, 25);
            Actividad FiestaPiscina = new ActividadHostal("Fiesta de la piscina", "Diviértete en la piscina y disfruta de una deliciosa barbacoa", new DateTime(2025, 7, 21, 12, 0, 0), 30, 21, "Alejandra Ramirez", "Zona de la piscina del hostal", true, 15);
            Actividad AvistamientoAves = new ActividadHostal("Excursión de aves", "Explora la fauna local y observa aves en su hábitat natural", new DateTime(2025, 7, 23, 6, 0, 0), 8, 10, "Santiago Perez", "Reserva natural del hostal", true, 40);
            Actividad JuegosMesa = new ActividadHostal("Juegos de mesa", "Juega tus juegos de mesa favoritos en una divertida noche en el hostal", new DateTime(2025, 7, 24, 20, 0, 0), 20, 12, "Carla Fernandez", "Área común del hostal", false);
            Actividad Caminata = new ActividadHostal("Caminata", "Disfruta de la naturaleza en una caminata", new DateTime(2025, 7, 28, 9, 0, 0), 8, 16, "Pablo Garcia", "Parque del hostal", true, 20);

            //------actividades-terciarizadas------//
            Actividad TourBici = new ActividadTerciarizada("Tour en bicicleta", "Recorre los puntos turísticos más importantes de la ciudad en bicicleta", new DateTime(2025, 7, 11, 10, 0, 0), 15, 12, DreamWorks, false, costo: 30);
            Actividad PaseoBarco = new ActividadTerciarizada("Paseo en barco", "Descubre la ciudad desde una perspectiva diferente en un relajante paseo en barco", new DateTime(2025, 7, 11, 14, 0, 0), 20, 8, Rekreation, true, new DateTime(2023, 7, 13), 50);
            Actividad VisitaVinedo = new ActividadTerciarizada("Visita a un viñedo", "Aprende sobre la producción de vino y degusta los mejores vinos de la región en un viñedo local", new DateTime(2025, 7, 12, 11, 0, 0), 10, 18, ElectricBlue, true, new DateTime(2023, 7, 12), 100);
            Actividad ExcursionParque = new ActividadTerciarizada("Excursión", "Admira la belleza natural de un parque nacional con un guía experto en la materia", new DateTime(2025, 7, 13, 9, 0, 0), 25, 10, DreamWorks, true, new DateTime(2023, 7, 15), 80);
            Actividad ClaseSurf = new ActividadTerciarizada("Clase de surf", "Aprende a surfear las olas del mar en una clase impartida por instructores certificados", new DateTime(2025, 7, 14, 13, 0, 0), 8, 16, TravelFun, false, costo: 60);
            Actividad DegustacionChocolate = new ActividadTerciarizada("Degustación de chocolate", "Descubre los secretos del chocolate y prueba diferentes variedades en una degustación especializada", new DateTime(2025, 7, 15, 15, 0, 0), 25, 10, AlonsoUmp, true, new DateTime(2023, 7, 20), 40);
            Actividad EscaladaRoca = new ActividadTerciarizada("Escalada en roca", "Desafía tus límites en una experiencia de escalada en roca guiada por expertos", new DateTime(2025, 7, 16, 23, 0, 0), 1, 18, DreamWorks, true, new DateTime(2023, 7, 18), 100);
            Actividad GloboAerostático = new ActividadTerciarizada("Paseo", "Disfrute de las vistas panorámicas en un paseo en globo", new DateTime(2025, 7, 15, 8, 0, 0), 6, 12, AlonsoUmp, true, new DateTime(2023, 7, 27, 12, 0, 0), 1000);
            Actividad CataVinos = new ActividadTerciarizada("Tour de cata", "Amplia variedad de vinos para degustar", new DateTime(2025, 7, 20, 10, 0, 0), 12, 18, Rekreation, true, new DateTime(2023, 7, 21, 9, 0, 0), 500);
            Actividad ClaseCocina = new ActividadTerciarizada("Clase de cocina", "Aprenda a cocinar platos típicos de la región", new DateTime(2025, 8, 5, 11, 0, 0), 8, 16, Ludica, false, costo: 250);
            Actividad CaminataSelva = new ActividadTerciarizada("Caminata nocturna", "Explore la fauna y flora nocturna de la selva tropical", new DateTime(2025, 9, 1, 18, 0, 0), 10, 18, TravelFun, true, new DateTime(2023, 9, 1, 10, 0, 0), 200);
            Actividad ExcursionKayak = new ActividadTerciarizada("Excursión en kayak", "Explore la costa en kayak y observe la vida marina", new DateTime(2025, 10, 10, 9, 0, 0), 6, 14, DreamWorks, true, new DateTime(2023, 10, 15, 14, 0, 0), 300);
            Actividad TourHistorico = new ActividadTerciarizada("Tour histórico", "Recorra el centro histórico y aprenda sobre la cultura local", new DateTime(2025, 7, 16, 23, 0, 0), 15, 12, TravelFun, false, costo: 50);
            Actividad Esqui = new ActividadTerciarizada("Esquí en la Nieve", "Actividad de deportes de invierno en una hermosa montaña nevada", new DateTime(2025, 08, 05, 9, 00, 00), 8, 16, Ludica, true, new DateTime(2023, 08, 06), 1200);
            Actividad TourBodegas = new ActividadTerciarizada("Tour de Bodegas", "Excursión para conocer bodegas de vinos", new DateTime(2025, 09, 20, 11, 00, 00), 15, 21, AlonsoUmp, false, costo: 900);
            Actividad PaseoVelero = new ActividadTerciarizada("Paseo en Velero", "Actividad de navegación en un velero por la costa", new DateTime(2025, 10, 05, 13, 00, 00), 6, 12, Rekreation, true, new DateTime(2023, 10, 06), 1500);
            Actividad Parapente = new ActividadTerciarizada("Parapente", "Experimenta la emoción de volar en parapente sobre las majestuosas montañas", new DateTime(2025, 7, 16, 18, 30, 0), 4, 18, ElectricBlue, true, new DateTime(2023, 7, 19), 120);
            Actividad Buceo = new ActividadTerciarizada("Buceo en la playa", "Explora el mundo submarino de la playa", new DateTime(2025, 7, 11, 11, 0, 0), 8, 16, DreamWorks, true, new DateTime(2023, 7, 15), 150);
            Actividad Safari = new ActividadTerciarizada("Safari en la selva", "Adéntrate en la selva y descubre la vida silvestre que habita allí", new DateTime(2025, 8, 10, 7, 0, 0), 0, 10, Ludica, true, new DateTime(2023, 8, 25), 200);
            Actividad ParqueAcuatico = new ActividadTerciarizada("Parque acuático", "Pasa un día emocionante en un parque acuático con toboganes, piscinas y mucho más", new DateTime(2025, 10, 2, 10, 0, 0), 20, 5, ElectricBlue, true, new DateTime(2023, 10, 20), 50);

            //------agendas------//            
            PrecargarAgendas(Yoga, huesped); //AL CONFIRMAR ESTA AGENDA, LA ACTIVIDAD RESULTA EN CAPACIDAD COLMADA.
            PrecargarAgendas(ExcursionBicicleta, huesped); //AL CONFIRMAR ESTA AGENDA, LA ACTIVIDAD RESULTA EN CAPACIDAD DISPONIBLE.
            PrecargarAgendas(EscaladaRoca, huesped); //AL CONFIRMAR ESTA AGENDA, LA ACTIVIDAD RESULTA EN CAPACIDAD COLMADA.
            PrecargarAgendas(Parapente, huesped); //AL CONFIRMAR ESTA AGENDA, LA ACTIVIDAD RESULTA EN CAPACIDAD DISPONIBLE.

            AgregarProveedor(DreamWorks);
            AgregarProveedor(EstelaUmp);
            AgregarProveedor(TravelFun);
            AgregarProveedor(Rekreation);
            AgregarProveedor(AlonsoUmp);
            AgregarProveedor(ElectricBlue);
            AgregarProveedor(Ludica);
            AgregarProveedor(Gimenez);
            AgregarProveedor(ProvX);
            AgregarProveedor(NorbertoMol);

            AgregarActividad(Senderismo);
            AgregarActividad(Yoga);
            AgregarActividad(Cine);
            AgregarActividad(TourGastronomico);
            AgregarActividad(TallerArtesania);
            AgregarActividad(ExcursionBicicleta);
            AgregarActividad(FiestaPiscina);
            AgregarActividad(AvistamientoAves);
            AgregarActividad(JuegosMesa);
            AgregarActividad(Caminata);

            AgregarActividad(TourBici);
            AgregarActividad(PaseoBarco);
            AgregarActividad(VisitaVinedo);
            AgregarActividad(ExcursionParque);
            AgregarActividad(ClaseSurf);
            AgregarActividad(DegustacionChocolate);
            AgregarActividad(EscaladaRoca);
            AgregarActividad(GloboAerostático);
            AgregarActividad(CataVinos);
            AgregarActividad(ClaseCocina);
            AgregarActividad(CaminataSelva);
            AgregarActividad(ExcursionKayak);
            AgregarActividad(TourHistorico);
            AgregarActividad(Esqui);
            AgregarActividad(TourBodegas);
            AgregarActividad(PaseoVelero);
            AgregarActividad(Parapente);
            AgregarActividad(Buceo);
            AgregarActividad(Safari);
            AgregarActividad(ParqueAcuatico);



            try
            {
                AgregarUsuario(op1);
            }
            catch
            {

            }

            try
            {
                AgregarUsuario(op2);
            }
            catch
            {

            }

            try
            {
                AgregarUsuario(huesped);
            }
            catch
            {

            }                      
        }

        //-----------------metodo agregar actividad-----------------//
        public void AgregarActividad(Actividad actividad)
        {
            try
            {
                actividad.Validar();

                if (ListaActividades.Contains(actividad))
                {
                    throw new Exception("Error al agregar actividad.");
                }

                ListaActividades.Add(actividad);
            }
            catch
            {
                
            }            
        }

        //-----------------metodo agregar proveedor-----------------//
        public void AgregarProveedor(Proveedor proveedor)
        {
            try
            {
                proveedor.Validar();                
                
                if (ListaProveedores.Contains(proveedor))
                {
                    throw new Exception("Ya existe un proveedor con las mismas credenciales.");
                }

                ListaProveedores.Add(proveedor);
            }
            catch
            {

            }
        }

        //-----------------metodo unicamente para la precarga de actividades-----------------//
        public void PrecargarAgendas(Actividad actividad, Huesped huesped)
        {
            try
            {
                Agenda agenda = new Agenda();

                agenda.AgregarHuesped(huesped);
                agenda.AgregarActividad(actividad);
                agenda.Validar();
                agenda.CalcularCostoFinal();
                agenda.AsignarEstado();
                agenda.IncrementarID();

                if (ListaAgendas.Contains(agenda))
                {
                    throw new Exception("Ya estas registrado a esta actividad.");
                }
                ListaAgendas.Add(agenda);
            }
            catch
            {

            }
        }

        //-----------------metodo agregar usuario-----------------//
        public void AgregarUsuario(Usuario usuario)
        {              
            if (ListaUsuarios.Contains(usuario))
            {
                throw new Exception("Ya existe un usario con las credenciales ingresadas.");
            }

            usuario.Validar();
            ListaUsuarios.Add(usuario);
        }
        
        public void AltaHuesped(Huesped hue)
        {            
            hue.Rol = "HUESPED";
            
            AgregarUsuario(hue);
        }

        public Usuario LoguearUsuario(Usuario usuario)
        {
            foreach(Usuario u in ListaUsuarios)
            {
                if(u.Correo == usuario.Correo && u.Contraseña == usuario.Contraseña)
                {
                    //HAY UN USUARIO LOGUEADO
                    return u;
                }
            }
            throw new Exception("Usuario y/o contraseña invalido.");
        }

        //-----------------metodo listar actividad-----------------//
        public List<Actividad> ListarActividades(DateTime fecha)
        {
            if(fecha == DateTime.MinValue)
            {
                fecha = DateTime.Now;
            }

            List<Actividad> listaConFiltro = new List<Actividad>();
            foreach(Actividad a in ListaActividades)
            {
                if(a.Fecha.Date == fecha.Date && a.ObtenerConfirmacion())
                {                    
                    listaConFiltro.Add(a);
                }
            }

            if(listaConFiltro.Count == 0)
            {
                throw new Exception($"No hay actividades programadas para la fecha {fecha.Date.ToShortDateString()}");
            }
            return listaConFiltro;
        }

        //-----------------metodo agendar actividad-----------------//
        public Agenda AgendarActividad(Actividad actividad, Huesped huesped)
        {   
            Agenda agenda = new Agenda();
 
            agenda.AgregarHuesped(huesped);
            agenda.AgregarActividad(actividad);
            agenda.Validar();
            agenda.CalcularCostoFinal();
            agenda.AsignarEstado();
            agenda.IncrementarID();

            if (ListaAgendas.Contains(agenda))
            {
                throw new Exception("Ya estas registrado a esta actividad.");
            }

            ListaAgendas.Add(agenda);
            return agenda;

        }

        //-----------------metodo obtener huesped por email-----------------//
        public Usuario ObtenerUsuarioPorEmail(string email)
        {
            Usuario retorno = null!;
            foreach(Usuario u in ListaUsuarios)
            {
                if(u.Correo ==  email)
                {
                    retorno = u;
                }
            }
            return retorno;
        }

        //-----------------metodo obtener agenda por email-----------------//
        public List<Agenda> ObtenerAgendaPorEmail(string email)
        {
            List<Agenda> aux = new List<Agenda>();

            foreach(Agenda a in ListaAgendas)
            {
                if(a.Huesped.Correo == email && a.FechaAgenda.Date >= DateTime.Now.Date)
                {
                    aux.Add(a);
                }
            }

            if(aux.Count == 0)
            {
                throw new Exception("No tienes actividades agendadas.");
            }
            aux.Sort();
            return aux;
        } 

        //-----------------metodo obtener actividad por id-----------------//
        public Actividad ObtenerActividadPorId(int idActividad)
        {
            Actividad retorno = null!;
            foreach(Actividad a in ListaActividades)
            {
                if(a.Id == idActividad) 
                {
                    retorno = a;
                }
            }
            return retorno;
        }

        //-----------------metodo obtener agenda por fecha-----------------//
        public List<Agenda> ObtenerAgendaPorFecha(DateTime fecha)
        {
            List<Agenda> aux = new List<Agenda>();

            foreach(Agenda a in ListaAgendas)
            {
                if(a.Actividad.Fecha.Date == fecha.Date)
                {
                    aux.Add(a);
                }
            }

            if(aux.Count == 0)
            {
                throw new Exception("No hay agendas para la fecha ingresada");
            }
            aux.Sort();
            return aux;
        }

        //-----------------metodo obtener agenda por documento-----------------//
        public List<Agenda> ObtenerAgendaPorDocumento(TipoDocumento tipo, string numero)
        {
            List<Agenda> aux = new List<Agenda>();

            foreach (Agenda a in ListaAgendas)
            {
                if(a.Huesped.TipoDocumento == tipo && a.Huesped.NroDocumento == numero)
                {
                    aux.Add(a);
                }
            }

            if(aux.Count == 0)
            {
                throw new Exception("No existen agendas para los documentos ingresados");
            }

            aux.Sort();
            return aux;
        }

        //-----------------metodo obtener agenda por id-----------------//
        public Agenda ObtenerAgendaPorId(int Id)
        {
            Agenda agenda = null!;

            foreach(Agenda a in ListaAgendas)
            {
                if(a.Id == Id)
                {
                    agenda = a;
                }
            }

            if(agenda == null)
            {
                throw new Exception("No se encontro una agenda con el id indicado.");
            }
            return agenda;
        }

        public List<Agenda> ObtenerAgendasPendientes()
        {
            List<Agenda> aux = new List<Agenda>();
            foreach (Agenda a in ListaAgendas)
            {
                if(a.Estado == "PENDIENTE_PAGO")
                {
                    aux.Add(a);
                }
            }

            if(aux.Count == 0)
            {
                throw new Exception("No hay agendas pendientes de pago.");
            }
            aux.Sort();
            return aux;
        }

        //-----------------metodo listar proveedor-----------------//
        public List<Proveedor> ListarProveedoresAlfabeticamente()
        {
            List<Proveedor> ListaProveedoresOrdenada = new List<Proveedor>(ListaProveedores);
            if (ListaProveedoresOrdenada.Count == 0)
            {
                throw new Exception("No existen proveedores en el sistema.");
            }

            ListaProveedoresOrdenada.Sort();
            return ListaProveedoresOrdenada;
        }

        //-----------------metodo obtener proveedor por id-----------------//
        public Proveedor ObtenerProveedorPorId(int numProv)
        {
            Proveedor retorno = null!;
            foreach (Proveedor p in ListaProveedores) 
            {
                if (p.NumeroProveedor == numProv)
                {
                    retorno = p;
                }
            }

            if(retorno == null)
            {
                throw new Exception("No existe un proveedor con el id indicado.");
            }
            return retorno;
        }

        //-----------------METODOS USADOS EN LA PARTE 1 Y NO EN LA PARTE 2-----------------//

        /*public void EstablecerPromocion(int numeroProveedor, int porcentajePromo)
        {
            bool establecido = false;
            foreach (Proveedor prov in ListaProveedores)
            {                
                if(prov.NumeroProveedor == numeroProveedor && !establecido)
                {
                    prov.EstablecerPromocion(porcentajePromo);
                    establecido = true;                    
                }
            }
            
            if (!establecido)
            {
                throw new Exception("No existe un proveedor con el numero indicado en nuestra base de datos.");
            }
        }

        //-----------------metodo listar actividades(fecha y costo)-----------------//
        public List<Actividad> ListarActividadesEspecificas(DateTime fecha1, DateTime fecha2, int costo)
        {
            List<Actividad> ListaAux = new List<Actividad>();

            if(costo < 0)
            {
                throw new Exception("El costo debe ser mayor o igual a 0.");
            }

            foreach (Actividad actividad in ListaActividades)
            {
                //---EVALUA SI LA FECHA ESTA COMPRENDIDA DENTRO DEL RANGO Y SI EL PRECIO ES MAYOR AL DADO---//
                if (fecha1 <= actividad.Fecha && fecha2 >= actividad.Fecha && actividad.Costo >= costo || fecha1 >= actividad.Fecha && fecha2 <= actividad.Fecha && actividad.Costo >= costo)
                {
                    ListaAux.Add(actividad);
                }
            }            
            return ListaAux.OrderByDescending(precio => precio.Costo).ToList();
        }*/
    }
}
