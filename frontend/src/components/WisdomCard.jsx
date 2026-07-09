// Import styles and date formatter
import { formatDate } from "../utils/dateFormatter";
import "../styles/WisdomCard.css";

// Define a set of colors for the wisdom cards
const COLORS = ["#2b2d64", "#5b3a8e", "#8b5e3c", "#1f6f6f", "#7a1f3d"];

// Function for determined wisdom card color
export function getWisdomColor(index) {
    return COLORS[index % COLORS.length];
}

// Card component
export default function WisdomCard({ wisdom, index, onEdit, onDelete }) {
    return (
        <div className="wisdom-card" style={{ backgroundColor: getWisdomColor(index) }}>
            <p className="wisdom-content">{wisdom.content}</p>
            <div className="wisdom-footer">
                <span className="wisdom-date">{formatDate(wisdom.createdOn)}</span>
                <div className="wisdom-actions">
                    <button onClick={() => onEdit(wisdom)} title="Update">✎</button>
                    <button onClick={() => onDelete(wisdom)} title="Delete">🗑</button>
                </div>
            </div>
        </div>
    );
}