
interface Props
{
    onFeedButtonClick: () => void;
    onResetButtonClick: () => void;
}

function ActionsPanel(props: Props) {
    return (
        <div className="action-bar">
            <h2>actions:</h2>
            <div className="action-bar-actions">
                <div className="button" onClick={() => props.onFeedButtonClick()}>
                    feed
                </div>
                <div className="button" onClick={() => props.onResetButtonClick()}>
                    reset
                </div>
            </div>
        </div>
    )
}

export default ActionsPanel;