using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class EventManager
{
    // --- DICCIONARIO DE NOMBRES EXACTOS ---
    private static string[] nombresNodos = new string[]
    {
        "",                             // 0 - (Vacío, no lo usamos)
        "Event_1",                      // 1 - Inicio
        "Event_2",                      // 2 - Duende Mercader
        "Event_3",                      // 3 - Venganza Duende
        "Event_4",         // 4 - Venganza Hombre Encapuchado
        "Event_5",    // 5 - Emboscada Ladrones
        "Event_6",   // 6 - Emboscada Sectarios
        "Event_7",         // 7 - Placa de presión
        "Event_8",           // 8 - Sala de saqueo
        "Event_9",        // 9 - Guarida Dragón
        "Event_10"  // 10 - Dragón Final
    };

    private static List<int> eventosDisponibles = new List<int>();

    private static bool fuerzaGuaridaDragon = false;
    private static bool fuerzaDragonCaraACara = false;
    private static bool fuerzaVenganzaHombre = false;
    private static bool juegoTerminado = false;

    [YarnFunction("NextEvent")]
    public static string NextEvent(int numeroEventoActual, int totalEventosPartida, bool condicionDuende, bool condicionHombre)
    {
        if (numeroEventoActual == 1)
        {
            fuerzaGuaridaDragon = false;
            fuerzaDragonCaraACara = false;
            fuerzaVenganzaHombre = false;
            juegoTerminado = false;

            eventosDisponibles = new List<int> { 2, 5, 6, 7, 8, 9 };
            if (condicionDuende) eventosDisponibles.Add(3);

            return nombresNodos[1];
        }

        if (juegoTerminado)
        {
            return nombresNodos[11];
        }

        if (fuerzaVenganzaHombre)
        {
            fuerzaVenganzaHombre = false;
            juegoTerminado = true;
            return nombresNodos[4];
        }

        if (fuerzaDragonCaraACara)
        {
            fuerzaDragonCaraACara = false;

            if (condicionHombre) fuerzaVenganzaHombre = true;
            else juegoTerminado = true;

            return nombresNodos[10];
        }

        if (fuerzaGuaridaDragon)
        {
            fuerzaGuaridaDragon = false;
            fuerzaDragonCaraACara = true;

            if (eventosDisponibles.Contains(9)) eventosDisponibles.Remove(9);
            return nombresNodos[9];
        }

        if (numeroEventoActual >= totalEventosPartida)
        {
            if (condicionHombre) fuerzaVenganzaHombre = true;
            else juegoTerminado = true;

            return nombresNodos[10];
        }

        return ObtenerEventoRandom();
    }

    private static string ObtenerEventoRandom()
    {
        if (eventosDisponibles.Count == 0) return nombresNodos[2];

        int indexAleatorio = Random.Range(0, eventosDisponibles.Count);
        int eventoElegido = eventosDisponibles[indexAleatorio];

        eventosDisponibles.RemoveAt(indexAleatorio);

        if (eventoElegido == 6) fuerzaGuaridaDragon = true; // Sectarios -> Guarida
        else if (eventoElegido == 9) fuerzaDragonCaraACara = true; // Guarida -> Dragón

        return nombresNodos[eventoElegido];
    }

    [YarnFunction("HasSuccess")]
    public static bool HasSuccess(int percentageOfSuccess)
    {
        int randomValue = Random.Range(0, 100);
        return randomValue < percentageOfSuccess;
    }
}