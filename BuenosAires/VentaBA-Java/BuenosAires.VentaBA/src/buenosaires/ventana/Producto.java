/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package buenosaires.ventana;

/**
 *
 * @author patri
 */
public class Producto {
    private int idprod;
    private String nomprod;
    private String descprod;
    private double precio;
    private String imagen;
    private int cantidad;
    private String disponibilidad;

    // Getters y setters
    public int getIdprod() { return idprod; }
    public void setIdprod(int idprod) { this.idprod = idprod; }

    public String getNomprod() { return nomprod; }
    public void setNomprod(String nomprod) { this.nomprod = nomprod; }

    public String getDescprod() { return descprod; }
    public void setDescprod(String descprod) { this.descprod = descprod; }

    public double getPrecio() { return precio; }
    public void setPrecio(double precio) { this.precio = precio; }

    public String getImagen() { return imagen; }
    public void setImagen(String imagen) { this.imagen = imagen; }

    public int getCantidad() { return cantidad; }
    public void setCantidad(int cantidad) { this.cantidad = cantidad; }

    public String getDisponibilidad() { return disponibilidad; }
    public void setDisponibilidad(String disponibilidad) { this.disponibilidad = disponibilidad; }
}