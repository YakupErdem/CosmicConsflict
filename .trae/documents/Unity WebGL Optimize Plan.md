## Mevcut Davranış
- Kalan soru yoksa yeni soru baloncuğu spawn EDİLMİYOR.
- Kanıtlar:
  - `QuestionBalloonSpawner.Spawn()` içinde gate: `if (!QuestionManager.instance.HasRemainingQuestions()) return;` (`CosmicConflict/Assets/Scripts/Ekstralar/QuestionBalloonSpawner.cs:16`).
  - `GameManager.QuestionSpawnLoop()` içinde gate ve durdurma: `if (!QuestionManager.instance.HasRemainingQuestions()) yield break;` (`CosmicConflict/Assets/Scripts/GameManager.cs:77`).

## Plan
- Ek değişiklik gerekmiyor; mevcut gate’ler ihtiyacı karşılıyor.
- İstersen ek failsafe olarak spawner’ı devre dışı bırakma (aktif/pasif) mekanizması ekleyebiliriz; onay halinde uygularım.

Onaylıyorsan bu haliyle bırakıyorum; başka bir davranış ister misin (ör. tüm sorular bitince belirli bir mesaj/puan)?