public class SceneReaction : Reaction {
    public string sceneName;
    public string startingPointInLoadedScene;
    public SaveData playerSaveData;

    private SceneController sceneController;

    protected override void SpecificInit() {
        sceneController = FindObjectOfType<SceneController>();
    }

    protected override void ImmediateReaction() {
        //playerSaveData.Save();
        sceneController.FadeAndLoadScene(this);
    }
}