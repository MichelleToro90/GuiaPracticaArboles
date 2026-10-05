using System;
using System.Windows.Forms;
using System.Drawing;

namespace GuiaPracticaArboles
{
    public class Nodo
    {
        public int Clave { get; set; }
        public string Valor { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public Nodo(int clave, string valor)
        {
            Clave = clave;
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    public class ArbolBinarioBusqueda
    {
        public Nodo Raiz { get; private set; }

        public void Insertar(int clave, string valor)
        {
            Raiz = InsertarRecursivo(Raiz, clave, valor);
        }

        private Nodo InsertarRecursivo(Nodo actual, int clave, string valor)
        {
            if (actual == null) return new Nodo(clave, valor);
            if (clave < actual.Clave)
                actual.Izquierdo = InsertarRecursivo(actual.Izquierdo, clave, valor);
            else if (clave > actual.Clave)
                actual.Derecho = InsertarRecursivo(actual.Derecho, clave, valor);
            return actual;
        }

        // Método para poblar visualmente un TreeView de Windows Forms
        public void LlenarTreeView(Nodo nodo, TreeNodeCollection nodesCollection)
        {
            if (nodo == null) return;

            TreeNode nuevoNodo = new TreeNode($"{nodo.Clave} - {nodo.Valor}");
            nodesCollection.Add(nuevoNodo);

            if (nodo.Izquierdo != null || nodo.Derecho != null)
            {
                // Si falta un hijo, podemos mostrar un marcador o recursión normal
                if (nodo.Izquierdo != null)
                    LlenarTreeView(nodo.Izquierdo, nuevoNodo.Nodes);
                if (nodo.Derecho != null)
                    LlenarTreeView(nodo.Derecho, nuevoNodo.Nodes);
            }
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Creamos una ventana personalizada
            Form ventana = new Form
            {
                Text = "Gestor Visual de Árboles BST - UEA",
                Width = 600,
                Height = 450
            };

            ArbolBinarioBusqueda bst = new ArbolBinarioBusqueda();
            
            // Insertamos datos de prueba iniciales
            bst.Insertar(50, "Nodo Raíz");
            bst.Insertar(30, "Izquierdo A");
            bst.Insertar(70, "Derecho B");
            bst.Insertar(20, "Hoja Izq");
            bst.Insertar(40, "Hoja Der");

            // Control visual TreeView para mostrar el árbol jerárquicamente
            TreeView treeView = new TreeView
            {
                Location = new Point(20, 20),
                Width = 540,
                Height = 320
            };

            // Cargamos los datos en el componente visual
            bst.LlenarTreeView(bst.Raiz, treeView.Nodes);
            treeView.ExpandAll();

            ventana.Controls.Add(treeView);

            // Ejecutamos la aplicación gráfica
            Application.Run(ventana);
        }
    }
}