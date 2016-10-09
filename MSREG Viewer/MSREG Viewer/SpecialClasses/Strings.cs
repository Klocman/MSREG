namespace MSREG.Viewer.SpecialClasses
{
    public class Strings
    {
        public static readonly Strings Default = new Strings();

        public readonly string Connected = "Podłączony";

        public readonly string ConnectingToDevice = "Łączenie z urządzeniem przez port ";
        public readonly string ConnectingToPort = "Otwieranie portu ";

        public readonly string DataReceived = "Otrzymano dane: ";
        public readonly string DataReceiveFailed = "Błąd odczytu danych: ";
        public readonly string DeviceInfoParseFailed = "Odczytanie danych urządzenia się nie powiodło: ";
        public readonly string DeviceInfoReceived = "Otrzymano dane urządzenia: ";

        public readonly string DeviceInfoUnknownTag = "Nieznany element danych: ";
        public readonly string DisconnectByUser = "Rozłączenie przez użytkownika";
        public readonly string DisconnectDisposed = "MsregDevice disposed while connected";
        public readonly string Disconnected = "Odłączony";
        public readonly string DisconnectFailedToOpen = "Nie udało się otworzyć połączenia";
        public readonly string DisconnectFailedToRespond = "Urządzenie nie odpowiedziało";

        public readonly string DisconnectHeader = "Disconnected: ";
        public readonly string DisconnectingPort = "Port zamknięty";
        public readonly string DisconnectStoppedResponding = "Urządzenie przestało odpowiadać";
        public readonly string GraphCurveMeasurement = "Pomiar";
        public readonly string GraphCurveSetting = "Nastawa";

        public readonly string GraphHumidityTitle = "Regulacja wilgotności";
        public readonly string GraphHumidityY = "Wilgotność względna [RH%]";
        public readonly string GraphTemperatureTitle = "Regulacja temperatury";
        public readonly string GraphTemperatureY = "Temperatura [°C]";
        public readonly string GraphTimeX = "Czas";

        public readonly string Searching = "Trwa wyszukiwanie...";
        public readonly string SearchingFoundNoDevices = "Nie znaleziono żadnych urządzeń";
        public readonly string SearchingFoundNoPorts = "Nie wykryto żadnych portów";

        public readonly string TitleMsr33Panel = "Panel sterowania MSR33";

        public readonly string TryToSyncSignalFailed = "Brak odpowiedzi na sync";
    }
}