;**********************************************************************************************************************************
; Temat: Obliczanie przecięcia promienia z obiektami w przestrzeni trójwymiarowej dla generowania obrazu metodą śledzenia promieni.
; Opis: Program generuje obraz przedstawiającą scenę z losowo ułożonymi obiektami. Obraz jest generowany metodą śledzenia promieni,
; a jej punktem krytycznym jest obliczanie przecięcia promienia z obiektami, które zawiera scena. Obliczane jest ono zarówno w
; dwóch bibliotekach - w języku C# oraz języku asemblera.
; Autor: Wiktor Merta, Informatyka, rok 3, sem. 5, gr. 5, data: 24.10.2022
; Wersja: 0.1
;**********************************************************************************************************************************

.const
tMin REAL4 0.001			; minimalna odległość dla prawidłowego punktu przecięcia

.code
;**********************************************************************************************************************************
; float IntersectSphereAsm(Vector3 origin, Vector3 direction, Vector3 center, float radius, float tMax)
; Procedura wyznacza odległość (t), po przebyciu której promień napotka punkt przecięcia z ze sferą umieszczoną w przestrzeni lub
; zwróci informację o braku przecięcia. Obliczona wartość jest wykorzystywana do dalszych obliczeń w celu wygenerowania obrazu.
; Ulokowanie parametrów wejściowych:
; xmm0 - źródło promienia (Vector3)
; xmm1 - kierunek promienia (Vector3)
; xmm2 - środek sfery (Vector3)
; xmm3 - promień sfery (float)
; stos - maksymalna odległość (float)
;**********************************************************************************************************************************

IntersectSphereAsm PROC

MOV rcx, [rbp+48]			; kopiowanie ze stosu argumentu tMax
MOVD xmm4, rcx				; kopiowanie tMax do dolnych 32 bitów rejestru xmm4

MOVAPS xmm5, xmm0			; kopiowanie źródła promienia do rejestru xmm5
SUBPS xmm5, xmm2			; obliczanie wektora między początkiem promienia, a środkiem sfery

MOVAPS xmm0, xmm1			; kopiowanie kierunku promienia do rejestru xmm2
							; obliczanie iloczynu skalarnego kierunku promienia z samym sobą (parametr a równania)
DPPS xmm0, xmm0, 241		; i umieszczenie go w dolnych 32 bitach rejestru xmm0

							; obliczanie iloczynu skalarnego kierunku promienia z wektorem między środkiem sfery,
DPPS xmm1, xmm5, 241		; a początkiem promienia (parametr b równania) i umieszczenie go w dolnych 32 bitach rejestru xmm1

MOVAPS xmm2, xmm5			; kopiowanie wektora między środkiem sfery, a początkiem promienia
							; obliczanie iloczynu skalarnego wektora między środkiem sfery, a początkiem promienia
DPPS xmm2, xmm2, 241		; z samym sobą i umieszczenie go w dolnych 32 bitach rejestru xmm2
MULSS xmm3, xmm3			; obliczanie kwadratu promienia sfery
SUBSS xmm2, xmm3			; odejmowanie kwadratu promienia sfery od wyniku z xmm2 aby obliczyć parametr c równania

MULSS xmm2, xmm0			; obliczanie a * c
MOVSS xmm3, xmm1			; kopiowanie b do rejestru xmm3
MULSS xmm3, xmm3			; obliczanie kwadratu parametru b
SUBSS xmm3, xmm2			; obliczanie wyznacznika równania b * b - a * c

PXOR xmm2, xmm2				; zerowanie rejestru xmm2
COMISS xmm3, xmm2			; porównanie wyznacznika z 0
JC ReturnZero				; skok do wyjścia z procedury z t = 0 jeśli wyznacznik mniejszy niż 0

SQRTSS xmm3, xmm3			; obliczanie pierwiastka ze współczynnika równania
SUBSS xmm2, xmm1			; obliczanie -b w rejestrze xmm2
MOVSS xmm1, xmm2			; kopiowanie -b do rejestru xmm1
SUBSS xmm1, xmm3			; odejmowanie pierwiastka z wyznacznika od -b
DIVSS xmm1, xmm0			; dzielenie wyniku przez a aby otrzymać pierwsze rozwiązanie równania

COMISS xmm1, xmm4			; porównanie rozwiązania z tMax
JNB SecondSolution			; skok do obliczania drugiego rozwiązania jeżeli t => tMax
COMISS xmm1, tMin			; porówanie rozwiązania z tMin
JC SecondSolution			; skok do obliczania drugiego rozwiązania jeżeli t < tMin
MOVSS xmm0, xmm1			; ustawienie zwracanego parametru na aktualne rozwiązanie
RET

SecondSolution:
	ADDSS xmm2, xmm3		; dodawanie pierwiastka z wyznacznika do -b
	DIVSS xmm2, xmm0		; dzielenie wyniku przez a aby otrzymać drugie rozwiązanie równania

	COMISS xmm2, xmm4		; porównanie rozwiązania z tMax
	JNB ReturnZero			; skok do wyjścia z procedury z t = 0 jeżeli t => tMax
	COMISS xmm2, tMin		; porówanie rozwiązania z tMin
	JC ReturnZero			; skok do wyjścia z procedury z t = 0 jeżeli t < tMin
	MOVSS xmm0, xmm2		; ustawienie zwracanego parametru na aktualne rozwiązanie
	RET

ReturnZero:
	PXOR xmm0, xmm0			; zerowanie rejestru xmm0 odpowiedzialnego za zwracanie wyniku zmiennoprzecinkowego
	RET

IntersectSphereAsm ENDP
END