﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kundt_front_end.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class it22AutoverleihEntities : DbContext
    {
        public it22AutoverleihEntities()
            : base("name=it22AutoverleihEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tblAusstattung> tblAusstattung { get; set; }
        public virtual DbSet<tblAuto> tblAuto { get; set; }
        public virtual DbSet<tblBuchung> tblBuchung { get; set; }
        public virtual DbSet<tblEyecatcher> tblEyecatcher { get; set; }
        public virtual DbSet<tblHistorie> tblHistorie { get; set; }
        public virtual DbSet<tblKategorie> tblKategorie { get; set; }
        public virtual DbSet<tblKunde> tblKunde { get; set; }
        public virtual DbSet<tblLand> tblLand { get; set; }
        public virtual DbSet<tblLogin> tblLogin { get; set; }
        public virtual DbSet<tblMarke> tblMarke { get; set; }
        public virtual DbSet<tblMitarbeiter> tblMitarbeiter { get; set; }
        public virtual DbSet<tblPLZOrt> tblPLZOrt { get; set; }
        public virtual DbSet<tblTreibstoff> tblTreibstoff { get; set; }
        public virtual DbSet<tblTyp> tblTyp { get; set; }
    
        public virtual int pBuchungAnlegen(Nullable<int> varIDKunde, Nullable<int> varIDAuto, string varBuchungVon, string varBuchungBis, Nullable<bool> varVersicherung, Nullable<bool> varStorno, ObjectParameter iDBuchung)
        {
            var varIDKundeParameter = varIDKunde.HasValue ?
                new ObjectParameter("varIDKunde", varIDKunde) :
                new ObjectParameter("varIDKunde", typeof(int));
    
            var varIDAutoParameter = varIDAuto.HasValue ?
                new ObjectParameter("varIDAuto", varIDAuto) :
                new ObjectParameter("varIDAuto", typeof(int));
    
            var varBuchungVonParameter = varBuchungVon != null ?
                new ObjectParameter("varBuchungVon", varBuchungVon) :
                new ObjectParameter("varBuchungVon", typeof(string));
    
            var varBuchungBisParameter = varBuchungBis != null ?
                new ObjectParameter("varBuchungBis", varBuchungBis) :
                new ObjectParameter("varBuchungBis", typeof(string));
    
            var varVersicherungParameter = varVersicherung.HasValue ?
                new ObjectParameter("varVersicherung", varVersicherung) :
                new ObjectParameter("varVersicherung", typeof(bool));
    
            var varStornoParameter = varStorno.HasValue ?
                new ObjectParameter("varStorno", varStorno) :
                new ObjectParameter("varStorno", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pBuchungAnlegen", varIDKundeParameter, varIDAutoParameter, varBuchungVonParameter, varBuchungBisParameter, varVersicherungParameter, varStornoParameter, iDBuchung);
        }
    
        [DbFunction("it22AutoverleihEntities", "fCarAvailable")]
        public virtual IQueryable<fCarAvailable_Result> fCarAvailable(string vonDate, string bisDate)
        {
            var vonDateParameter = vonDate != null ?
                new ObjectParameter("vonDate", vonDate) :
                new ObjectParameter("vonDate", typeof(string));
    
            var bisDateParameter = bisDate != null ?
                new ObjectParameter("bisDate", bisDate) :
                new ObjectParameter("bisDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fCarAvailable_Result>("[it22AutoverleihEntities].[fCarAvailable](@vonDate, @bisDate)", vonDateParameter, bisDateParameter);
        }
    
        public virtual ObjectResult<pCarAvailableFinal_Result> pCarAvailableFinal(string vonDate, string bisDate, string klasse, string sitzanzahl)
        {
            var vonDateParameter = vonDate != null ?
                new ObjectParameter("vonDate", vonDate) :
                new ObjectParameter("vonDate", typeof(string));
    
            var bisDateParameter = bisDate != null ?
                new ObjectParameter("bisDate", bisDate) :
                new ObjectParameter("bisDate", typeof(string));
    
            var klasseParameter = klasse != null ?
                new ObjectParameter("klasse", klasse) :
                new ObjectParameter("klasse", typeof(string));
    
            var sitzanzahlParameter = sitzanzahl != null ?
                new ObjectParameter("sitzanzahl", sitzanzahl) :
                new ObjectParameter("sitzanzahl", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pCarAvailableFinal_Result>("pCarAvailableFinal", vonDateParameter, bisDateParameter, klasseParameter, sitzanzahlParameter);
        }
    
        [DbFunction("it22AutoverleihEntities", "fCarAvailableFilterIncluded")]
        public virtual IQueryable<fCarAvailableFilterIncluded_Result> fCarAvailableFilterIncluded(string vonDate, string bisDate, string klasse, string sitzanzahl)
        {
            var vonDateParameter = vonDate != null ?
                new ObjectParameter("vonDate", vonDate) :
                new ObjectParameter("vonDate", typeof(string));
    
            var bisDateParameter = bisDate != null ?
                new ObjectParameter("bisDate", bisDate) :
                new ObjectParameter("bisDate", typeof(string));
    
            var klasseParameter = klasse != null ?
                new ObjectParameter("klasse", klasse) :
                new ObjectParameter("klasse", typeof(string));
    
            var sitzanzahlParameter = sitzanzahl != null ?
                new ObjectParameter("sitzanzahl", sitzanzahl) :
                new ObjectParameter("sitzanzahl", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fCarAvailableFilterIncluded_Result>("[it22AutoverleihEntities].[fCarAvailableFilterIncluded](@vonDate, @bisDate, @klasse, @sitzanzahl)", vonDateParameter, bisDateParameter, klasseParameter, sitzanzahlParameter);
        }
    }
}
