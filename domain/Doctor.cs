namespace Domain;
public class Doctor {
    public int ID { get; set; }
    public string FullName { get; set; } = "";
    public string Specialization { get; set; } = ""; // or enum ?

    public void Add() {}
    public void Delete() {}
    public void GetAll() {}
    public void Search() {}
}