# PhoeniciaHotels-Booking
App for booking PH rooms Phoenicia hotels booking -documentie-

1)Prezentare generala Phoenica hotels booking app reprezinta un technical fit simplist al Booking.com realizat exclusiv pentru hotelurile Phoenicia. Softul vine in ajutorul clientilor care inregistrandu-se pot face o rezervare la unul dintre hoteluri in functie de locatie, data check-in, data check-out, filtrand si sortand optiunile avute. Ce a de-a doua parte este alocata angajatilor care vor putea face legatura intre rezervari si camera, emite facturi etc. Momentan doar sectiunea destinata clientilor este disponibila. Odata ales un hotel clientul poate vizualiza descrierea hotelurilor, il poate identifica pe Google Maps (conexiune la internet necesara), poate vedea comentariile altor utilizatori , si poate lasa un review doar daca a rezervat in trecut o camera sau mai multe la hotelul respectiv. Acesta alege tipul camerelor si nr de camere in limita stocului disponibil pentru a realiza comanda .

Tehnologii Aplicatia Phoenicia Hotels Booking este de tipul Windows Form Application realizata in frame-work-ul .Net cu ajutorul limbajului C# . Am utilizat SQL Server pentru baza de date si frame-work-ul MetroFramework pentru design. In imaginea de mai jos se afla schema-ul tabelei principale Hotels.
3)Algoritmi si structure de date Pentru a sorta hoteluirile am implementat Selection Sort pe criteriile: stele, numarul de review-uri, pret.

Pentru destinatia aleasa se aplica distanta Levenshtein iar rezultatul acestui algoritm va ajuta utilizatorul in gasirea localitatii dorite pe baza denumirilor aflate in fisierul cities.txt. Distanta Levenstein este un algoritm ce poate ajuta la determinarea similaritatii intre expresii sau cuvinte utilizate in cautari. Functia poate fi folosita pentru a compara diverse deosebiri intre doua cuvinte. Prin distanta Levenstein se intelege numarul de caractere ce trebuie inlocuite, inserate, eliminate din prima expresie pentru a obtine a doua expresie.

In cadrul aplicatiei am folosit structuri de date omogene : vectori si matrice ,dar si structuri de date neomogene.
