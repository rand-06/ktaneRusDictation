using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class rusDictation : MonoBehaviour {

   public KMBombInfo Bomb;
   public KMAudio Audio;
   public KMSelectable[] TypableButtons;
   public KMSelectable StartButton;
   public KMSelectable ReturnButton;
   public TextMesh NumberDisplay;
   public TextMesh DisplayText;
   public GameObject TVStatusSphere;
   public Material[] StatusColors;
    public KMBombModule Module;


    private bool _moduleSelected;
   int DetonationNoiseCounter;
   int IndexOfSubmissionWord;
   int PhraseIndex;
   int RandomPhraseSubmissionIndex;

   float Fanfare;
   float DefaultTimePerWord = 0.05f;

   string QWERTYAlphabet = "QWERTYUIOPASDFGHJKLZXCVBNM<";
   private string[][] PhraseList = new string[][] {
      new string[] {"ты", "офигел", "меня", "заткнуть", "вздумал", "или", "я", "потвоему", "не", "имею", "права", "высказывать", "свое", "мнение", "Ни", "в", "коем", "случае", "я", "просто", "попросил", "это", "твое", "право", "Ты", "еще", "не", "высказывал", "свое", "мнение", "поэтому", "я", "готов", "слушать", "Я", "на", "80", "уверен", "что", "раньше", "по", "крайней", "мере", "в", "день", "введения", "фракций", "гдето", "прямым", "текстом", "было", "написано", "что", "мы", "против", "развития", "но", "щас", "этого", "нет", "Но", "доказательств", "нет", "к", "сожалению"},
      new string[] {"БЛЯТЬ", "ТЫ", "СУКА", "НЕ", "ПРЕДСТАВЛЯЕШЬ", "КАК", "Я", "ХОЧУ", "АСТРО", "Я", "ХОЧУ", "ЧТОБЫ", "ОН", "ОКУТАЛ", "МЕНЯ", "В", "СВОЕ", "ОДЕЛЯЛО", "И", "ПРИЖАЛСЯ", "КО", "МНЕ", "ВПЛОТНУЮ", "ДА", "ТАК", "ЧТОБ", "ЕГО", "КОЛЕНО", "ДОТРАГИВАЛАСЬ", "ДО", "МОЕЙ", "МОШОНКИ", "А", "ЗАТЕМ", "МНЕ", "ГОВОРИЛ", "ЛАСКОВЫЕ", "СЛОВА", "ШЕПЧА", "МНЕ", "В", "УХО", "Я", "ХОЧУ", "ЧТОБЫ", "ДВЕ", "ЕГО", "ПАРЫ", "РУК", "ЛАПАЛИ", "МОЕ", "ЧУВСТВИТЕЛЬНОЕ", "ТЕЛЬЦЕ", "ОТ", "ГРУДИ", "ДО", "НОГ", "ХОЧУ", "ЧТОБЫ", "ОН", "ЧАТЕЛЬНО", "РАССЛЕДОВАЛ", "СВОИМИ", "ГОЛУБЫМИ", "ЛАПАМИ", "МОЙ", "УЗКИЙ", "И", "МАЛЕНЬКИЙ", "АНУС", "ЧТОБЫ", "ОН", "СВОИМ", "ЧЛЕНОМ", "ВОШЕЛ", "В", "МЕНЯ", "СО", "ВСЕЙ", "СИЛЫ", "ПИЗДЕЦ", "Я", "ХОЧУ", "ЧТОБЫ", "ОН", "ТРАХАЛ", "МОЕ", "ОЧКО", "ТАК", "БУДТО", "ПОГРУЖАЕМСЯ", "В", "ЛЮБОВНЫЙ", "СОН", "А", "КОГДА", "ОН", "СТАНОВИТСЯ", "ТВИСТЕДОМ", "И", "ВЫПИВАЕТ", "ЦЕЛЫЕ", "300", "КАНИСТР", "С", "ИХОРОМ", "А", "ХОЧУ", "ЕГО", "СОЧНЫЙ", "ХУЙ", "ЕЩЕ", "СИЛЬНЕЕ", "НЕ", "НУ", "А", "ЧО", "ОГРОМНОЕ", "СТОЯЧЕЕ", "РОЗОВОЕ", "ЕЛДИЩЕ", "ИСПАЧКАНОЕ", "ИХОРОМ", "ТАК", "ЕЩЕ", "И", "СЕКСУАЛЬНО", "ДЕРГАЕТСЯ", "СУКА", "А", "ТЕЛО", "У", "НЕГО", "КАКОЕ", "БЛЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯТЬ", "ПИЗДЕЦ"},
      new string[] {"Вики", "говно", "тупое", "Я", "сидел", "кучу", "времени", "начинал", "уже", "винить", "себя", "что", "ошибки", "делаю", "а", "проблема", "в", "одном", "сраном", "условии", "написанном", "на", "Вики", "Репозиторий", "говно", "тупое", "Я", "сидел", "кучу", "времени", "начинал", "уже", "винить", "себя", "что", "ошибки", "делаю", "а", "проблема", "в", "одном", "сраном", "условии", "написанном", "на", "Репо"},
      new string[] {"Блин", "ну", "что", "здесь", "опять", "случилось", "Сколько", "бы", "я", "не", "пытался", "сделать", "все", "для", "людей", "нет", "все", "равно", "нормально", "вести", "себя", "не", "умеют", "Вопервых", "предлагать", "изменения", "и", "обсуждать", "их", "может", "каждый", "Поступило", "пожелание", "хорошо", "Почему", "бы", "не", "обсудить", "все", "культурно", "грамотно", "и", "конструктивно", "Зачем", "вести", "себя", "токсично", "писать", "и", "присылать", "в", "каналпредложку", "все", "эти", "мемнорофельнеы", "фразы", "гифки", "и", "картинки", "если", "они", "только", "портят", "ситуацию", "и", "захламляют", "чат", "И", "тем", "более", "участвовать", "в", "продолжении", "этой", "недодискуссии", "Особенно", "это", "касается", "участников", "BorisP", "Ранда", "и", "Джевила"},
      new string[] {"Привет", "мои", "милые", "я", "надеюсь", "вам", "хорошо", "спалось", "он", "ехидно", "улыбнулся", "и", "прищурил", "глаза", "Его", "вторая", "рука", "дрогнула", "доставая", "кухонный", "острый", "и", "блестящий", "нож", "Он", "крепко", "сжимал", "его", "и", "поставил", "кончик", "точно", "по", "середине", "груди", "близнецов", "Холод", "лезвия", "чувствовался", "даже", "при", "такой", "малой", "площади", "соприкосновения", "лезвия", "с", "кожей", "через", "одежду", "Мне", "всегда", "было", "интересно", "что", "будет", "если", "разъединить", "сиамских", "близнецов", "знаете", "кто", "выживет", "а", "кто", "нет", "Хотя", "ха", "из", "вас", "наверное", "никто", "не", "выживет", "у", "вас", "скорее", "всего", "одни", "органы", "по", "всему", "телу"},
      new string[] {"Но", "внезапно", "проснувшись", "у", "себя", "в", "кровати", "окутанный", "своим", "пледом", "и", "находясь", "в", "своей", "одежде", "он", "начал", "осматривать", "свое", "тело", "Царапин", "не", "было", "Он", "посмеялся", "а", "потом", "рассмеялся", "понимая", "что", "это", "был", "просто", "кошмар", "и", "он", "в", "порядке", "ничего", "не", "было", "Но", "на", "прикроватный", "тумбочке", "его", "привлек", "один", "неординарный", "предмет", "А", "Это", "валентинка", "Ох", "он", "и", "забыл", "что", "сегодня", "день", "святого", "Валентина", "Взяв", "ее", "он", "не", "увидел", "от", "кого", "она", "Открыв", "он", "побледнел", "и", "все", "таки", "почувствовал", "отчетливую", "боль", "в", "пояснице", "Дорогой", "Астро", "С", "днем", "святого", "Валентина", "Я", "рад", "написать", "тебе", "это", "после", "того", "что", "произошло", "в", "магазине", "я", "бы", "хотел", "повторить", "данное", "действо", "Ты", "был", "очень", "хорошим", "и", "послушным", "Люблю", "тебя", "мой", "милый", "луннолицый", "Денди", "с", "любовью", "3"},
      new string[] {"так", "вот", "эта", "поеботина", "под", "названием", "мунфлатер", "который", "прыгающий", "который", "блестящий", "который", "идет", "нахуй", "полный", "кусок", "кала", "я", "не", "собираюсь", "клирать", "эту", "только", "по", "причине", "существования", "данного", "отродья", "это", "пиздец", "я", "дултана", "создателя", "уровня", "нахуй", "на", "эту", "луну", "отправлю", "без", "еды", "и", "воды", "и", "пусть", "радуется", "что", "в", "его", "уровень", "может", "играть", "только", "мио", "на", "11x", "у", "меня", "нахуй", "глаза", "выпадают", "от", "этого", "уровня", "это", "подпрыгивание", "в", "начале", "заставляет", "меня", "делать", "ебанную", "чечетку", "от", "такого", "пиздеца", "после", "этого", "шедевральный", "геймплей", "с", "тапами", "который", "хуй", "прочитаешь", "даже", "в", "фтодж", "было", "приятнее", "чем", "в", "этом", "отродье", "дерьма", "и", "мочи", "и", "также", "шедевральный", "геймплей", "с", "блоками", "и", "инверсами", "который", "ты", "нахуй", "должен", "учить"},
      new string[] {"Лять", "я", "упомянул", "таймзоны", "пока", "с", "челиком", "одним", "говорил", "и", "моя", "душа", "залилась", "ненавистью", "Мне", "кажется", "это", "не", "нормально", "Знаете", "что", "еще", "ненормально", "Чертов", "Timezones", "Я", "его", "ненавижу", "Я", "порвать", "его", "готов", "Мне", "нужно", "успокоительное"},
      new string[] {"Бля", "мне", "нужна", "губозакаточная", "машинка", "срочно", "Подарите", "мне", "на", "др", "что", "случилось", "Карочее", "я", "в", "рул", "34", "наткнулся", "на", "обложку", "одного", "интересного", "комикса", "Если", "прям", "в", "кратцекратце", "то", "сюжет", "заключается", "в", "том", "что", "твистеды", "пускают", "Шримпо", "по", "кругу", "Я", "нашел", "автора", "в", "твиттере", "он", "уже", "начал", "его", "делать", "И", "вот", "моя", "раскатанная", "губа", "видна", "уже", "в", "Москве", "как", "вдруг", "оказывается", "что", "хуй", "там", "Точнее", "наоборот", "нет", "хуя", "там", "Этот", "челикс", "назначил", "ему", "пизду", "вместо", "члена", "Моей", "злости", "нет", "предела"},
      new string[] {"faulty", "sink", "my", "beloved", "ахахахахха", "upside", "down", "sink", "my", "fucking", "beloved", "I", "FUCKING", "LOVE", "STRIKING", "3", "TIMES", "TO", "FUCKING", "UPSIDE", "DOWN", "SINK", "ВСМЫСЛЕ", "ТЫ", "РЕШИЛ", "ПЕРЕВЕРНУТЫЙ", "СИНК", "ЗА", "5", "СЕКУНД", "КАКОГО", "ХУЯ", "Я", "НА", "НЕГО", "ДО", "МИНУТЫ", "ТРАЧУ", "ИДИ", "ТЫ", "а", "так", "тебе", "повезло", "опять", "блять", "решай", "дальше", "еще", "раз", "давай", "без", "вопросов", "или", "я", "сойду", "с", "ума", "короче", "пошло", "оно", "все", "нахуй", "я", "увольняюсь", "БЛЯТЬ", "ТАМ", "СВОЯ", "ТАБЛИЦА", "ДЛЯ", "ПЕРЕВЕРНУТОГО", "СУКА", "Я", "НЕ", "ВИДЕЛ"},
      new string[] {"ХУЙНЯ", "ВАШ", "WASTE", "MANAGEMENT", "ДНО", "ЕБАННОЕ", "КТО", "ТАК", "МАНУАЛЫ", "ПИШЕТ", "ДАЖЕ", "Я", "ПОНЯТНЕЕ", "ПИШУ", "bro", "thinks", "hes", "noob", "he", "is", "окей", "хорошо", "написано", "до", "ближайшего", "числа", "У", "МЕНЯ", "455", "ДО", "КАКОГО", "БЛИЖАЙШЕГО", "АЛЛО", "по", "правилам", "оокругления", "46", "где", "там", "хоть", "слово", "про", "правила", "округления", "помоему", "это", "логично", "так", "это", "сука", "даже", "не", "в", "переводе", "проблема", "то", "что", "не", "написано", "это", "конечно", "плохо", "но", "блин", "самому", "можно", "было", "догадаться", "в", "инглишь", "мануале", "так", "же", "дебильно", "написано", "КАКОЕ", "НАХУЙ", "ДОГАДАТЬСЯ", "АЛЛО", "Я", "УЖЕ", "НАДОГАДЫВАЛСЯ", "ДО", "ТОГО", "ЧТО", "СУКА", "НАЗНАЧИТЬ", "В", "ОСТАТКИ", "ЭТО", "КАК", "ОКАЗАЛОСЬ", "НЕ", "WASTE", "НАХУЙ", "А", "ПРОСТО", "ПРИБАВИТЬ"},
      new string[] {"3", "Изначально", "я", "хотел", "чтобы", "мне", "просто", "ушки", "подарили", "перчаткилапки", "неожиданно", "шли", "в", "комплекте", "с", "ними", "А", "еще", "я", "примерноверно", "привык", "к", "тому", "как", "я", "выгляжу", "поэтому", "обещаю", "фейс", "ривил", "на", "выходных", "этой", "либо", "следующей", "недели", "И", "это", "не", "очередной", "бросок", "слов", "на", "ветер", "3", "Считаем", "коэффициент", "пиздежа", "народ", "Ваши", "предположения", "Превысит", "курс", "доллара", "Ставлю", "свою", "девственность", "Как", "в", "случае", "проигрыша", "платить", "будешь", "Я", "заберу", "ой", "одобряю"},
      new string[] {"Такс", "я", "наконецто", "вернулся", "немножко", "освободился", "от", "дел", "и", "снова", "могу", "заняться", "сервером", "в", "дискорде", "Ну", "я", "на", "самом", "деле", "не", "пропадал", "прямотаки", "насовсем", "но", "просто", "не", "писал", "ничего", "так", "как", "был", "занят", "и", "не", "было", "времени", "Вопервых", "я", "очень", "рад", "всем", "новым", "участникам", "на", "сервере", "Вовторых", "есть", "несколько", "вещей", "о", "которых", "можно", "будет", "поговорить", "которые", "можно", "обсудить"},
      new string[] { "КАК", "ЖЕ", "ХОЧУ", "ЭТОГО", "СОЧНОГО", "ДЕНДИ", "КАК", "ЖЕ", "ХОЧУ", "ЧТОБЫ", "ОН", "МЕНЯ", "СУКА", "ПЫТАЛ", "В", "СВОЕЙ", "КРОВАТКЕ", "ИСПОЛЬЗУЯ", "РАЗЛИЧНЫЕ", "НЕПРИСТОЙНЫЕ", "ИГРУШКИ", "С", "РАЗМЕРОМ", "1933", "ТЕРРАМЕТРОВ", "ХОЧУ", "ОТСОСАТЬ", "ЕГО", "МОЩНЫЙ", "ИХОРСКИЙ", "СТРОЙНЫЙ", "ХУЙ", "ТАК", "ЧТОБЫ", "ОН", "ОТЪЕБАЛ", "МОЮ", "ГОЛОВУ", "ПОКА", "НЕ", "ИЗУРОДУЕТ", "ЕЁ", "КАК", "ЖЕ", "СУККККА", "ХОЧУ", "ТРОЙНИЧЁК", "С", "НИМ", "И", "АААААСТРОООООО", "ЧТОБЫ", "ДЕНДИ", "ДОМИНИРОВАЛ", "НАД", "НАМИ", "ЧТОБЫ", "ЖЁСТКО", "ДОЛБИЛ", "ЖОПУ", "АСТРО", "А", "Я", "ВЫЛИЗЫВАЛ", "ЕГО", "АНУС", "ОБЛИТЫЙ", "В", "ИХОР", "СУКАА", "Я", "ДАЖЕ", "ГОТОВ", "ТВИСТЕДОМ", "РАДИ", "НЕГО", "СТАТЬ", "ПИЗДЕЕЕЕЕЕЦ", "ТЫ", "НЕНАВИДЕШЬ", "ДЕНДИ", "ДА", "КАК", "ТЫ", "СУКА", "СМЕЕШЬ", "НЕБЛАГОДАРНОЕ", "ЧМОО", "БЛЯТЬ", "СГИНЬ", "В", "АДУ", "ЕСЛИ", "ЕГО", "НЕНАВИДИШЬ", "ДО", "КРАЙНОСТЕЙ", "У", "ТЕБЯ", "ДАЖЕ", "НЕТ", "ЛИЧНОСТИ", "ПОТОМУ", "ЧТО", "ТЫ", "НЕ", "ЛЮБИШЬ", "ДЭНДИ", "но", "неееет", "нетнентентетнетнетнтент", "НЕ", "НЕ", "НЕЕЕЕЕЕЕЕ", "МХАААХАХАХАХАХАХАХАХХАХАХАХАХА", "Я", "ЗАСТАВЛЮ", "СЛЫШИШЬ", "ЗАСТААААВЛЮ", "ТЕБЯ", "ВЛЮБИТЬСЯ", "В", "ДЕНДИ" },
      new string[] { "Бля", "ть", "вот", "меня", "щас", "мучает", "вопрос", "Помнишь", "тот", "прон", "с", "Губом", "да", "Как", "анон", "об", "его", "зубы", "член", "не", "поцарапал", "У", "Губа", "же", "острые", "зубы", "аккуратность", "Ого", "Аккуратно", "сосет", "не", "нуачо", "С", "расстановкой", "с", "выражением", "главное", "в", "минете", "аккуратно", "отсосать", "хуй", "не", "кусая", "его", "а", "то", "больно", "буде", "сложно", "поспорить", "как", "будто", "ты", "сам", "пробовал", "не", "довелось", "да", "и", "кто", "бы", "говорил", "кто", "кого", "здесь", "учит", "минет", "делать", "мы", "друг", "друга", "вопросы", "ответы", "оо", "69", "найс", "все", "крч", "ждем", "видеотуториал", "от", "Губа" }
   };
   string CurrentSubmission = "";
   string UnderscoresAwaitingSubmission = "";

   bool IsDisplayingPhrase;
   bool Solving;

   static int moduleIdCounter = 1;
   int moduleId;
   private bool moduleSolved;

   void Awake () {
      moduleId = moduleIdCounter++;
      foreach (KMSelectable Key in TypableButtons) {
         Key.OnInteract += delegate () { KeyPress(Key); return false; };
      }
      StartButton.OnInteract += delegate () { StartButtonPress(); return false; };
      ReturnButton.OnInteract += delegate () { ReturnPress(); return false; };
      
    }

   void Start () {
      PhraseIndex = UnityEngine.Random.Range(0, PhraseList.Count());
        GetComponent<KMSelectable>().OnFocus += delegate { _moduleSelected = true; };
        GetComponent<KMSelectable>().OnDefocus += delegate { _moduleSelected = false; };
    }

   void KeyPress (KMSelectable Key) {
      Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Key.transform);
      StartCoroutine(keyAnimation(Key));
      if (moduleSolved || Solving || IsDisplayingPhrase) {
         return;
      }
      Audio.PlaySoundAtTransform("Type3", transform);
      for (int i = 0; i < 27; i++) {
         if (Key == TypableButtons[i]) {
            if (i == 26 && CurrentSubmission.Length == 0) {
               return;
            }
            if (i == 26) {
               CurrentSubmission = CurrentSubmission.Substring(0, CurrentSubmission.Length - 1);
            }
            else {
               CurrentSubmission = CurrentSubmission + QWERTYAlphabet[i];
            }
            DisplayText.text = CurrentSubmission;
         }
      }
   }

    void KeyPressRus(char key)
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        if (moduleSolved || Solving || IsDisplayingPhrase)
        {
            return;
        }
        Audio.PlaySoundAtTransform("Type3", transform);
        CurrentSubmission = CurrentSubmission + key;
        DisplayText.text = CurrentSubmission;
    }

    void StartButtonPress () {
      StartButton.AddInteractionPunch();
      StartCoroutine(keyAnimation(StartButton));
      Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, StartButton.transform);
      if (moduleSolved || Solving) {
         return;
      }
      if (!IsDisplayingPhrase) {
         CurrentSubmission = "";
         StartCoroutine(DisplayPhrase());
      }
   }

   void ReturnPress () {
      ReturnButton.AddInteractionPunch();
      Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, ReturnButton.transform);
      StartCoroutine(keyAnimation(ReturnButton));
      if (moduleSolved || Solving || IsDisplayingPhrase) {
         return;
      }
      else if (PhraseList[PhraseIndex][IndexOfSubmissionWord].ToLower() == CurrentSubmission.ToLower()) {
         Audio.PlaySoundAtTransform("Dick", transform);
         moduleSolved = true;
         StartCoroutine(SolveAnimation());
      }
      else {
         GetComponent<KMBombModule>().HandleStrike();
         Debug.LogFormat("[Изложение #{0}] You submitted {1}. Strike, fasiofjnaldnfal.", moduleId, CurrentSubmission);
         DisplayText.text = "Incorrect";
         NumberDisplay.text = "!";
         CurrentSubmission = "";
      }
   }

   IEnumerator DisplayPhrase () {
      TVStatusSphere.GetComponent<MeshRenderer>().material = StatusColors[1];
      IsDisplayingPhrase = true;
      for (int i = 0; i < PhraseList[PhraseIndex].Count(); i++) {
         DisplayText.text = PhraseList[PhraseIndex][i];
         DefaultTimePerWord += (float) PhraseList[PhraseIndex][i].Length * .09f;
         yield return new WaitForSecondsRealtime(DefaultTimePerWord);
         DisplayText.text = "";
         yield return new WaitForSecondsRealtime(DefaultTimePerWord / 2);
         DefaultTimePerWord = 0.05f;
         UnderscoresAwaitingSubmission = "";
      }
      TVStatusSphere.GetComponent<MeshRenderer>().material = StatusColors[0];
      IndexOfSubmissionWord = UnityEngine.Random.Range(0, PhraseList[PhraseIndex].Count());
      for (int i = 0; i < PhraseList[PhraseIndex][IndexOfSubmissionWord].Length; i++) {
         UnderscoresAwaitingSubmission += "-";
      }
      DisplayText.text = UnderscoresAwaitingSubmission;
      NumberDisplay.text = (IndexOfSubmissionWord + 1).ToString();
      IsDisplayingPhrase = false;
      string Logmessage = "";
      for (int i = 0; i < PhraseList[PhraseIndex].Count(); i++) {
         Logmessage += PhraseList[PhraseIndex][i] + " ";
      }
      Debug.LogFormat("[Изложение #{0}] The message is \"{1}\".", moduleId, Logmessage);
      Debug.LogFormat("[Изложение #{0}] It asks for word number {1}, which is \"{2}\".", moduleId, IndexOfSubmissionWord + 1, PhraseList[PhraseIndex][IndexOfSubmissionWord]);
   }

   IEnumerator SolveAnimation () {
      Solving = true;
      RandomPhraseSubmissionIndex = UnityEngine.Random.Range(0, PhraseList.Count());
      DisplayText.text = PhraseList[RandomPhraseSubmissionIndex][UnityEngine.Random.Range(0, PhraseList[RandomPhraseSubmissionIndex].Count())];
      NumberDisplay.text = (UnityEngine.Random.Range(0, 100)).ToString();
      yield return new WaitForSeconds(Fanfare / 1000);
      Audio.PlaySoundAtTransform("Type3", transform);
      Fanfare += 1f;
      TypableButtons[UnityEngine.Random.Range(0, 27)].OnInteract();
      if (Fanfare >= 125f) {
         Audio.PlaySoundAtTransform("Dick", transform);
         Debug.LogFormat("[Изложение #{0}] You submitted the correct word. Module disarmed.", moduleId);
         GetComponent<KMBombModule>().HandlePass();
         NumberDisplay.text = "69";
         DisplayText.text = "ахахах\nстакан)))))";
         Solving = false;
         yield break;
      }
      StartCoroutine(SolveAnimation());
   }

   private IEnumerator keyAnimation (KMSelectable Button) {
      Button.AddInteractionPunch(0.125f);
      Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
      for (int i = 0; i < 4; i++) {
         Button.transform.localPosition += new Vector3(0, -0.001f, 0);
         yield return new WaitForSeconds(0.005F);
      }
      for (int i = 0; i < 4; i++) {
         Button.transform.localPosition += new Vector3(0, +0.001f, 0);
         yield return new WaitForSeconds(0.005F);
      }
   }

    string unQWERTY = "qwertyuiop[]asdfghjkl;'zxcvbnm,.1234567890";
    string QWERTY = "QWERTYUIOPASDFGHJKLZXCVBNM";
    string unQWERTYrus = "ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ1234567890";

    void Update()
    {
        if (_moduleSelected)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (CurrentSubmission.Length!=0)
                {
                    CurrentSubmission = CurrentSubmission.Substring(0, CurrentSubmission.Length - 1);
                    DisplayText.text = CurrentSubmission;
                }
            }
            for (var ltr = 0; ltr < 42; ltr++)
                if (Input.GetKeyDown(unQWERTY[ltr].ToString()))
                {
                    KeyPressRus(unQWERTYrus[ltr]);
                }
        }
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use (!{0} старт) to start the message. Use (!{0} отправить XXXX) to submit the word.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
       yield return null;
        command = command.ToUpper();
        if (command == "СТАРТ")
        {
            StartButton.OnInteract();
        }
        else if (Regex.IsMatch(command, "ОТПРАВИТЬ ([А-Я0-9A-Z]+)"))
        {
            string word = command.Substring(10);

            for (int i = 0; i< word.Length; i++)
            {
                if (word[i] > 'Z' || word[i] < 'A') KeyPressRus(word[i]);
                else KeyPress(TypableButtons[QWERTY.IndexOf(word[i])]);
                yield return new WaitForSeconds(0.05f);
            }
            ReturnPress();
        }
        else yield return "sendtochaterror Invalid command.";
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return SolveAnimation();        
    }
}
