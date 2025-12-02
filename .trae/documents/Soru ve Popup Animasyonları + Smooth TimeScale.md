## Kapsam
- Sadece sahne içindeki soru/popup UI akışı güncellenecek.
- `SceneChanger` ve `scene loader` hiçbir şekilde değiştirilmeyecek.

## Hedefler
- Soru paneli ve cevap sonrası popup animasyonla açılıp kapanacak.
- Oyun durdurma/başlatma `Time.timeScale` 0.5 sn’de 1→0 ve 0→1 olacak şekilde yumuşatılacak.
- Sahne içindeki diğer menü/paneller de aynı popup animasyon stilini kullanabilecek (isteğe bağlı bağlama), ama sadece sahne içi.

## Teknik Yaklaşım
- Animator varsa tetik ("Open"/"Close"), yoksa script tabanlı `CanvasGroup + localScale` animasyonu kullanılır.
- `Time.timeScale` geçişleri `unscaled delta time` ile çalışan coroutine’ler üzerinden yapılır.

## Yeni/İyileştirilen Bileşenler
1. PopupAnimator (yeni script)
- API: `Show(GameObject panel)` / `Hide(GameObject panel)`.
- Davranış:
  - Panelde `Animator` varsa `SetTrigger("Open"/"Close")` tetikler.
  - Yoksa `CanvasGroup.alpha` 0→1 ve `transform.localScale` 0.9→1.0 animasyonunu unscaled zamanda uygular.

2. TimeScaleSmoother (yeni yardımcı)
- API: `PauseSmooth(0.5f)` / `ResumeSmooth(0.5f)`.
- `Time.timeScale` değerini 0.5 saniyede yumuşak geçişle değiştirir; unscaled zaman kullanır.

## Entegrasyon Noktaları (Sahne İçi)
- QuestionManager (`CosmicConflict/Assets/Scripts/QuestionManager.cs`)
  - `TryOpenQuestion()`
    - Önce `PopupAnimator.Show(panel)` ile soru panelini animasyonla aç.
    - Ardından `TimeScaleSmoother.PauseSmooth(0.5f)` ile oyunu yumuşakça durdur.
  - `Close()`
    - `PopupAnimator.Hide(panel)` ile paneli animasyonla kapat.
    - `TimeScaleSmoother.ResumeSmooth(0.5f)` ile oyunu yumuşakça başlat.
  - `ShowPopup(string message)`
    - `PopupAnimator.Show(popupPanel)` ile popup’ı animasyonla aç.
    - `WaitForSecondsRealtime(popupDuration)` sonrası `PopupAnimator.Hide(popupPanel)` ile kapat.
  - Doğru cevap puanı (mevcut): `GameManager.AddPoint(30)` korunur.

- QuestionBalloon / QuestionBalloonSpawner (mevcut sahne içi akış)
  - Kod değişimi gerekmez; balon vurulduğunda `QuestionManager.TryOpenQuestion()` zaten çağrılıyor.

## UI/Asset
- İstersen soru ve popup panellerine Animator bağlanıp `Open`/`Close` klipleri atanır.
- Animator yoksa script animasyonu varsayılan olarak çalışır; `CanvasGroup` eklenir.

## Doğrulama
- Balon vurulunca soru paneli animasyonla açılır; 0.5s’de timeScale 1→0 olur.
- Cevap sonrası panel kapanır, popup animasyonla görünür; süre dolunca kapanır.
- Ardından 0.5s’de timeScale 0→1 döner.

Onay verirsen bu sahne içi değişiklikleri uygulayıp test edeceğim; `SceneChanger`/`scene loader` dosyalarına dokunulmayacak.