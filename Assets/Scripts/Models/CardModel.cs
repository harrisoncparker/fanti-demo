using System;

[Serializable]
public class CardModel : Model
{
    // Basic Fields
    public string front;
    public string back;
    public bool doubleSided;

    // Spaced Repition Fields
    public int reviewCount = 0; // Number of successful reviews in a row
    public float easinessFactor = 2.5f; // Default EF
    public int intervalDays = 1;

    // Serialized Timestamps
    public string lastReview;
    public DateTime LastReviewDateTime
    {
        get => DeserializeDateTime(lastReview);
        set => lastReview = value.ToString("o");
    }

    // Non Serialized Timestamps
    public DateTime NextReviewDateTime
    {
        get => LastReviewDateTime.AddDays(intervalDays);
    }

    public CardModel(string front, string back, bool doubleSided = false)
    {
        this.front = front;
        this.back = back;
        this.doubleSided = doubleSided;

        LastReviewDateTime = DateTime.Now.Subtract(TimeSpan.FromDays(intervalDays));
    }

    public void Review(int score)
    {
        if (score < 3) 
        {
            // If user struggles (quality 0-2), reset interval
            reviewCount = 0;
            intervalDays = 1;
        }
        else
        {
            if (reviewCount == 0) intervalDays = 1;
            else if (reviewCount == 1) intervalDays = 6;
            else intervalDays = (int)Math.Round(intervalDays * easinessFactor);

            reviewCount++;
        }

        // Update EF using SuperMemo's formula
        easinessFactor = Math.Max(1.3f, easinessFactor + (0.1f - (5 - score) * (0.08f + (5 - score) * 0.02f)));

        // Update last review
        LastReviewDateTime = DateTime.Now;
    }
}
