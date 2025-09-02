public interface ITextBackgroundFadeController {
    public void FadeIn(float deltaTime);
    public void FadeOut(float deltaTime);
    public float FadeTime {get;}
}