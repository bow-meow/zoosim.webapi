import { useState, useEffect } from "react"

interface Props
{
    time: Date;
    hoursPassed: number;
    onHourPassed: () => void;
    onSecondPassed: () => void;
}

function TimePanel(props: Props) {

    const MAX_FATIGUE_TIME = 20
    const [fatigueTime, setFatigueTime] = useState(1);

    useEffect(() => {
        setInterval(() => {
            setFatigueTime(prev => {
                const timeInSeconds = prev + 1;
                if (timeInSeconds == MAX_FATIGUE_TIME) {
                    return 1;
                }
                return timeInSeconds;
            });

            props.onSecondPassed();
        }, 1000);

        setInterval(() => {
            props.onHourPassed();
        }, 19000);
    }, [])

    return (
        <>
            <h1>info:</h1>
            <div className="action-bar-time-details">
                <h2>time:</h2>
                <h1>{props.time.toLocaleTimeString()}</h1>
            </div>
            <div className="action-bar-time-details">
                <h2>time untill fatigue:</h2>
                <h1>{fatigueTime}</h1>
            </div>
            <div className="action-bar-time-details">
                <h2>hours passed:</h2>
                <h1>{props.hoursPassed}</h1>
            </div>
        </>
    )
}

export default TimePanel;