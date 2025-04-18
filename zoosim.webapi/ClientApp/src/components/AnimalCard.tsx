import { Animal } from "../api"

interface Props
{
    animal: Animal
}
function AnimalCard(props: Props) {
    return (
        <div className="animal-card">
            <img src={props.animal.isAlive ? props.animal.aliveImg.src : props.animal.deadImg.src}
                 alt={props.animal.isAlive ? props.animal.aliveImg.alt : props.animal.deadImg.alt} />
            <span className="animal-status">{props.animal.canMove ? "can move" : "cant move"}</span>
            <div className="bar-fill"
                style={{
                    width:`${props.animal.health}%`
                }}
            >
                {`${props.animal.health}%`}
            </div>
        </div>
    )
}

export default AnimalCard;