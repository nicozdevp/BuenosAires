/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package buenosaires.proxy;

import buenosaires.proxy2.IWsProductoJava;
import java.util.ArrayList;
import buenosaires.proxy2.WsProductoJava;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import java.lang.reflect.Type;

/**
 *
 * @author patri
 */
public class ScStockProducto {
    public String accion = "";
    public String mensaje = "";
    public Boolean hayErrores = false;
    public String jsonStockProducto = "";
    //public Producto Producto = null;//
    //public List<Producto> Lista = null;// 
    public ArrayList<StockProducto> lista = new ArrayList<StockProducto>();

    public ScStockProducto() {
        accion = "";
        mensaje = "";
        hayErrores = false;
        jsonStockProducto = "";
        lista = null;
    }

    public String getAccion() {
        return accion;
    }

    public void setAccion(String accion) {
        this.accion = accion;
    }

    public String getMensaje() {
        return mensaje;
    }

    public void setMensaje(String mensaje) {
        this.mensaje = mensaje;
    }

    public Boolean getHayErrores() {
        return hayErrores;
    }

    public void setHayErrores(Boolean hayErrores) {
        this.hayErrores = hayErrores;
    }

    public String getJsonStockProducto() {
        return jsonStockProducto;
    }

    public void setJsonStockProducto(String jsonStockProducto) {
        this.jsonStockProducto = jsonStockProducto;
    }

    public ArrayList<StockProducto> getLista() {
        return lista;
    }

    public void setLista(ArrayList<StockProducto> lista) {
        this.lista = lista;
    }
    
    public void ejecutarPruebas() {
        try {
        // Llamar al WebService
        WsProductoJava ws = new WsProductoJava();
        IWsProductoJava port = ws.getBasicHttpBindingIWsProductoJava();

        // Llamar al método del WS
        buenosaires.proxy2.Respuesta resp1 = port.productosJava();
        String json1 = resp1.getJsonStockProducto().getValue();

        // Guardar el JSON como referencia (opcional)
        this.jsonStockProducto = json1;

        // Deserializar el JSON en lista de productos
        Gson gson = new Gson();
        Type tipoLista = new TypeToken<ArrayList<StockProducto>>() {}.getType();
        this.lista = gson.fromJson(json1, tipoLista);

    } catch (Exception e) {
        System.err.println("Error al cargar productos: " + e.getMessage());
        this.lista = new ArrayList<>(); // evitar null si falla
    }
    }

    @Override
    public String toString() {
        return "ScStockProducto{" + "accion=" + accion + ", mensaje=" + mensaje + ", hayErrores=" + hayErrores + ", jsonStockProducto=" + jsonStockProducto + ", lista=" + lista + '}';
    }

    
    
    

    
    
}
