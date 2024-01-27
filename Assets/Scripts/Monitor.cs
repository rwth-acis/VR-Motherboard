using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Monitor : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI questionText;


    [SerializeField] private TextMeshProUGUI answer1;
    [SerializeField] private TextMeshProUGUI answer2;
    [SerializeField] private TextMeshProUGUI answer3;

    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;

    private Component currentComponent = Component.NONE;

    private AudioSource audio;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        void addListener(TextMeshProUGUI answer, int i)
        {
            // Add a new EventTrigger component if not already present
            if (answer.GetComponent<EventTrigger>() == null)
            {
                answer.gameObject.AddComponent<EventTrigger>();
            }

            answer.GetComponent<EventTrigger>().triggers.Clear();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => {
                OnAnswerClick(i);
            });
            answer.GetComponent<EventTrigger>().triggers.Add(entry);
        }

        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => OnAnswerClick(1));
        button2.onClick.AddListener(() => OnAnswerClick(2));
        button3.onClick.AddListener(() => OnAnswerClick(3));

        // addListener(answer3, 3);
    }

    private void OnButtonClicked(SelectEnterEventArgs args)
    {
        Debug.Log(args.interactorObject.ToString());
    }

    public void OnAnswerClick1() => OnAnswerClick(1);

    private void OnAnswerClick(int i)
    {
        Question question = currentComponent.Quiz.Question;
        bool correct = question.CheckAnswer(i);

        if (correct) audio.clip = correctSound;
        else audio.clip = incorrectSound;

        audio.Play();
        GameObject.Find("Score").GetComponent<Score>().OnAnswer(question, correct);

        Debug.Log("Click " + i);

        currentComponent.Quiz.NextQuestion();
        UpdateQuiz();
    }


    public void OnChangeComponent(Component component)
    {
        Start();
        currentComponent = component;
        UpdateQuiz();
    }

    private void UpdateQuiz()
    {
        if (currentComponent == Component.NONE)
        {
            questionText.text = "You have to put a component in the beam to play the quiz!";

            answer1.text = "";
            answer2.text = "";
            answer3.text = "";
        }
        else
        {
            Question question = currentComponent.Quiz.Question;

            questionText.text = question.Title;

            answer1.text = question.Answers[0].Text;
            answer2.text = question.Answers[1].Text;
            answer3.text = question.Answers[2].Text;
        }
    }
}

public class Quiz
{
    public List<Question> Questions { get; set; }

    public Quiz(List<Question> questions)
    {
        Questions = questions;
    }

    private int index = 0;

    public Question Question
    {
        get { return Questions[index]; }
    }

    public Question NextQuestion()
    {
        int i = (index + 1);
        if (i >= Questions.Count) index = 0;
        else index = i;

        return Questions[index];
    }

    public static readonly Quiz GPU = new Quiz(new List<Question> {
        new Question("What does GPU stand for?", new List<Answer> {
            new Answer("General Processing Unit", false),
            new Answer("Gigabyte Processing Unit", false),
            new Answer("Graphics Processing Unit", true),
        }),
        new Question("Which of the following is the primary function of a GPU?", new List<Answer> {
            new Answer("Managing storage", false),
            new Answer("Processing graphics and visuals", true),
            new Answer("Running operating systems", false),
        }),
        new Question("What is the purpose of a GPU driver?", new List<Answer> {
            new Answer("Facilitating communication between the operating system and the GPU", true),
            new Answer("Controlling the mouse and keyboard", false),
            new Answer("Managing power supply to the GPU", false),
        }),
    });

    public static readonly Quiz CPU = new Quiz(new List<Question> {
        new Question("What does CPU stand for?", new List<Answer> {
            new Answer("Central Processing Unit", true),
            new Answer("Computer Processing Unit", false),
            new Answer("Central Performance Unit", false),
        }),
        new Question("What is the main function of a CPU in a computer?", new List<Answer> {
            new Answer("Storing data permanently", false),
            new Answer("Executing instructions and performing calculations", true),
            new Answer("Managing graphics and visuals", false),
        }),
        new Question("What is the purpose of hyper-threading in a CPU?", new List<Answer> {
            new Answer("Managing power consumption", false),
            new Answer("Improving multitasking performance by simulating multiple cores", true),
            new Answer("Controlling cooling systems", false),
        }),
    });

    public static readonly Quiz RAM = new Quiz(new List<Question> {
        new Question("What does RAM stand for?", new List<Answer> {
            new Answer("Random Access Memory", true),
            new Answer("Read-Only Memory", false),
            new Answer("Rapid Access Memory", false),
        }),
        new Question("What is the primary function of RAM in a computer?", new List<Answer> {
            new Answer("Providing long-term storage for files", false),
            new Answer("Temporarily storing data for quick access by the CPU", true),
            new Answer("Managing network connections", false),
        }),
        new Question("What is the measurement unit commonly used for RAM capacity?", new List<Answer> {
            new Answer("Gigahertz", false),
            new Answer("Gigabytes", true),
            new Answer("Megapixels", false),
        }),
    });

    public static readonly Quiz FAN = new Quiz(new List<Question> {
        new Question("What is the primary function of a CPU fan?", new List<Answer> {
            new Answer("Providing power to the CPU", false),
            new Answer("Cooling the CPU to prevent overheating", true),
            new Answer("Enhancing the CPU's processing speed", false),
        }),
        new Question("Why is it important to keep the CPU temperature within a certain range?", new List<Answer> {
            new Answer("To prolong the lifespan of the CPU", true),
            new Answer("To increase the CPU's clock speed", false),
            new Answer("To improve the CPU's multitasking capabilities", false),
        }),
        new Question("What can happen if a CPU becomes too hot due to inadequate cooling?", new List<Answer> {
            new Answer("Increased system stability", false),
            new Answer("Thermal throttling or system shutdown to prevent damage", true),
            new Answer("Improved gaming performance", false),
        }),
    });

    public static readonly Quiz DEFAULT = new Quiz(new List<Question> {
        new Question("Frage 1", new List<Answer> {
            new Answer("Antwort 1", true),
            new Answer("Antwort 2", false),
            new Answer("Antwort 3", false),
        }),
    });
}

public class Question
{
    public string Title { get; set; }
    public List<Answer> Answers { get; set; }

    public Question(string title, List<Answer> answers)
    {
        Title = title;
        Answers = answers;
    }

    public bool CheckAnswer(int index)
    {
        return Answers[index - 1].IsCorrect;
    }

    public bool CheckAnswer(Answer answer)
    {
        return answer.IsCorrect;
    }
}

public class Answer
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }

    public Answer(string text, bool isCorrect)
    {
        Text = text;
        IsCorrect = isCorrect;
    }
}