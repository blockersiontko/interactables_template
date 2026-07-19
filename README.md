# interactables_template

## First Person Interaction Prototype

Prototyp gry w widoku pierwszoosobowym (FPP) w Unity, oparty o system interakcji ze scenowymi obiektami za pomocą raycastingu. Gracz porusza się po planszy i może wchodzić w interakcje z obiektami takimi jak dźwignia, drzwi czy skrzynia, z których każdy reaguje w inny sposób.

## Technologie

- Unity Engine z Universal Render Pipeline (URP) - widoczne po komponencie UniversalAdditionalCameraData na kamerze oraz Global Volume z profilem post processingu.
- C# / .NET jako język skryptowy oparty o UnityEngine.MonoBehaviour.
- System fizyki Unity - Rigidbody, CapsuleCollider, BoxCollider oraz MeshCollider do obsługi kolizji i ruchu opartego o fizykę.
- Raycasting (Physics.Raycast) jako mechanizm wykrywania obiektów, z którymi można wejść w interakcję.
- Interfejsy w C# (IInteractable) jako podstawa polimorficznego systemu interakcji.
- Input System (legacy) - Input.GetAxis, Input.GetAxisRaw, Input.GetMouseButtonDown do obsługi klawiatury i myszy.

## Logika projektu

Projekt składa się z dwóch niezależnych, współpracujących ze sobą systemów: poruszania się gracza oraz interakcji ze światem.

### Poruszanie się i kamera

- `playerMovement` odpowiada za ruch postaci. W `Update()` odczytuje surowe wejście z klawiatury (Horizontal, Vertical) i przelicza je na kierunek ruchu względem orientacji obiektu gracza (transform.forward, transform.right). W `FixedUpdate()` ustawia prędkość liniową Rigidbody bezpośrednio, z wyłączoną grawitacją i zablokowaną rotacją fizyczną, dzięki czemu ruch jest w pełni kontrolowany przez skrypt.
- `CameraControl` obsługuje rozglądanie się myszą w typowym dla gier FPP stylu: kamera (dziecko obiektu gracza) obraca się w osi pionowej z ograniczeniem kątów do przedziału -90/+90 stopni, natomiast obrót w poziomie jest przekazywany do całego obiektu gracza (`playerBody.Rotate`). Kursor myszy jest blokowany i ukrywany na starcie rozgrywki.

### System interakcji

- `IInteractable` definiuje wspólny kontrakt (metoda `Interact()`) dla wszystkich obiektów, z którymi gracz może wejść w interakcję. Takie podejście pozwala dodawać nowe typy interaktywnych obiektów bez modyfikowania kodu odpowiedzialnego za samo wykrywanie interakcji.
- `Interactor`, umieszczony na graczu, w każdej klatce nasłuchuje lewego przycisku myszy. Po kliknięciu wysyła promień (Ray) ze środka ekranu w kierunku patrzenia kamery (`Camera.main.ScreenPointToRay`) i sprawdza, czy trafiony obiekt posiada komponent implementujący `IInteractable`. Jeśli tak, wywołuje na nim metodę `Interact()`.
- Trzy klasy implementują `IInteractable`, każda z własną logiką:
  - **Lever** - po interakcji zmienia kolor materiału na czerwony i włącza emisję (`_EMISSION`), tworząc efekt świecącej się dźwigni.
  - **Door** - po każdej interakcji losuje nowy kolor RGB i przypisuje go do materiału obiektu, symulując np. zmianę stanu drzwi poprzez zmianę wyglądu.
  - **Chest** - przy pierwszej interakcji obraca się o 90 stopni i przesuwa swoją pozycję, symulując otwieranie wieka skrzyni. Działa jednorazowo dzięki wewnętrznej fladze stanu, która blokuje ponowne otwarcie.

## Struktura sceny

| Obiekt | Rola | Kluczowe komponenty |
|---|---|---|
| Player | Gracz, rodzic kamery | Rigidbody, CapsuleCollider, playerMovement, Interactor |
| Main Camera | Kamera FPP, dziecko Playera | Camera, AudioListener, CameraControl |
| Directional Light | Oświetlenie sceny | Light |
| Global Volume | Profil post processingu URP | Volume |
| Lever | Interaktywna dźwignia | BoxCollider, MeshRenderer, Lever |
| Door | Interaktywne drzwi | BoxCollider, MeshRenderer, Door |
| Chest / ChestBase | Interaktywna skrzynia i jej podstawa | BoxCollider, MeshRenderer, Chest |
| Plane | Podłoga, kolizja dla gracza | MeshCollider |

## Wymagania

- Unity z Universal Render Pipeline (URP)
- .NET / C#

## Uruchomienie

1. Otwórz scenę w Unity i wejdź w tryb Play.
2. Poruszaj się klawiszami W/A/S/D, rozglądaj się myszą.
3. Kliknij lewym przyciskiem myszy patrząc na dźwignię, drzwi lub skrzynię, aby wejść z nimi w interakcję.

## Znane problemy

- W komentarzach w CameraControl widoczne są zniszczone polskie znaki diakrytyczne, co wskazuje na zapis plików w niewłaściwym kodowaniu (brak UTF-8). Warto zapisać wszystkie skrypty w UTF-8.
- Nazwa prywatnego pola `_Slopidelko` w klasie Chest jest nieopisowa i utrudnia czytelność kodu, warto zmienić ją na coś w stylu `_isOpen`.
- Brak informacji zwrotnej dla gracza (np. UI z podpowiedzią "naciśnij aby wejść w interakcję") przed samym kliknięciem.
