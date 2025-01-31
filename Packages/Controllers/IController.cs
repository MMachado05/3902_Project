namespace Project; // TODO: Namespace

public interface IController
{
    /// <summary>
    /// Check input states for this controller's device. Requires accessing
    /// device states directly (i.e. no State structs are passed as parameters)
    /// </summary>
    void ProcessControls();
}
