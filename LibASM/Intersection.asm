.code
IntersectSphereAsm PROC

MOV rcx, [rbp+48]			; kopiowanie ze stosu argumentu tMax
MOVD xmm4, rcx				; kopiowanie tMax do dolnych 32 bitów rejestru xmm4

							; xmm0 - źródło promienia (Vector3)
							; xmm1 - kierunek promienia (Vector3)
							; xmm2 - środek sfery (Vector3)
							; xmm3 - promień sfery (float)
							; xmm4 - maksymalne t (float)

MOVAPS xmm5, xmm0			; kopiowanie źródła promienia do rejestru xmm5
SUBPS xmm5, xmm2			; obliczanie wektora między początkiem promienia, a środkiem sfery

MOVAPS xmm2, xmm1			; kopiowanie kierunku promienia do rejestru xmm2
MULPS xmm2, xmm2			; obliczanie kwadratu wektora kierunku
MOVAPS xmm0, xmm2			; kopiowanie wyniku do rejestru xmm6
PSHUFD xmm2, xmm2, 225		; zamiana słów aby na pierwszej pozycji był y
ADDSS xmm0, xmm2			; dodawanie y do x
PSHUFD xmm2, xmm2, 210		; zamiana słów aby na pierwszej pozycji był z	
ADDSS xmm0, xmm2			; dodawanie z do wyniku x + y aby obliczyć parametr a

MOVAPS xmm2, xmm1			; kopiowanie kierunku promienia do rejestru xmm2
MULPS xmm2, xmm5			; obliczanie iloczynu wektora kierunku oraz wektora między początkiem promienia a środkiem sfery
MOVAPS xmm1, xmm2			; kopiowanie wyniku do rejestru xmm1
PSHUFD xmm2, xmm2, 225		; zamiana słów aby na pierwszej pozycji był y
ADDSS xmm1, xmm2			; dodawanie y do x
PSHUFD xmm2, xmm2, 210		; zamiana słów aby na pierwszej pozycji był z	
ADDSS xmm1, xmm2			; dodawanie z do wyniku x + y aby obliczyć parametr b

MULPS xmm5, xmm5			; obliczanie kwadratu wektora między między początkiem promienia a środkiem sfery
MOVAPS xmm2, xmm5			; kopiowanie wyniku do rejestru xmm2
PSHUFD xmm5, xmm5, 225		; zamiana słów aby na pierwszej pozycji był y
ADDSS xmm2, xmm5			; dodawanie y do x
PSHUFD xmm5, xmm5, 210		; zamiana słów aby na pierwszej pozycji był z	
ADDSS xmm2, xmm5			; dodawanie z do wyniku x + y
MULSS xmm3, xmm3			; obliczanie kwadratu promienia sfery
SUBSS xmm2, xmm3			; odejmowanie kwadratu promienia sfery od wyniku aby obliczyć parametr c

MULSS xmm2, xmm0			; obliczanie a * c
MOVSS xmm3, xmm1			; kopiowanie b do rejestru xmm3
MULSS xmm3, xmm3			; obliczanie kwadratu parametru b
SUBSS xmm3, xmm2			; obliczanie wyznacznika równania b * b - a * c

PXOR xmm2, xmm2				; zerowanie rejestru xmm2
COMISS xmm3, xmm2			; porównanie wyznacznika z 0
JC ReturnZero				; skok jeśli wyznacznik mniejszy niż 0

SQRTSS xmm3, xmm3			; obliczanie pierwiastka ze współczynnika równania

SUBSS xmm2, xmm1			; obliczanie -b w rejestrze xmm2
MOVSS xmm2, xmm1			; kopiowanie -b do rejestru xmm1
SUBSS xmm1, xmm3			; odejmowanie pierwiastka z wyznacznika od -b
DIVSS xmm1, xmm0			; dzielenie wyniku przez a

COMISS xmm1, xmm4			; porównanie rozwiązania z tMax
JNB SecondSolution			; skok jeżeli t => tMax
MOV rcx, 981668463			; utworzenie w rcx wartości tMin
MOVD xmm5, rcx				; kopiowanie do xmm5 zawartości rcx aby otrzymać 0.001
COMISS xmm1, xmm5			; porówanie rozwiązania z tMin
JC SecondSolution			; skok jeżeli t < tMin
MOVSS xmm0, xmm1			; ustawienie zwracanego parametru na aktualne rozwiązanie
RET

SecondSolution:
	ADDSS xmm2, xmm3		; dodawanie pierwiastka z wyznacznika do -b
	DIVSS xmm2, xmm0		; dzielenie wyniku przez a

	COMISS xmm2, xmm4		; porównanie rozwiązania z tMax
	JNB ReturnZero			; skok jeżeli t => tMax
	COMISS xmm2, xmm5		; porówanie rozwiązania z tMin
	JC ReturnZero			; skok jeżeli t < tMin
	MOVSS xmm0, xmm2		; ustawienie zwracanego parametru na aktualne rozwiązanie
	RET

ReturnZero:
	PXOR xmm0, xmm0			; zerowanie rejestru xmm0
	RET

IntersectSphereAsm ENDP
END