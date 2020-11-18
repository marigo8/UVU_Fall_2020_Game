using UnityEngine;
using Substance.Game;

public class SubstanceBehaviour : MonoBehaviour
{
    public SubstanceGraph graph;
    public int scratchSpeed = 500;

    public void AddScratches()
    {
        var scratches = graph.GetInputInt("Metal_Scratches_Amount");
        scratches += (int) (scratchSpeed * Time.fixedDeltaTime);
        graph.SetInputInt("Metal_Scratches_Amount", scratches);
        graph.QueueForRender();
        Substance.Game.Substance.RenderSubstancesAsync();
    }
}
