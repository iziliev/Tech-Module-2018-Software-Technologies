package softuniBlog.entity;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "articles")
public class Article {

    private Integer id;

    private String title;

    private String picture;

    private String content;

    private User author;


    public Article(String title, String picture, String content, User author) {
        this.title = title;
        this.picture = picture;
        this.content = content;
        this.author = author;

    }

    public Article() { }

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    @Column(columnDefinition = "text", nullable = false)
    public void setTitle(String title) {
        this.title = title;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    @ManyToOne()
    @JoinColumn(nullable = false, name = "authorId")
    public User getAuthor() {
        return author;
    }

    public void setAuthor(User author) {
        this.author = author;
    }

    @Transient
    public String getSummary(){
        return this.getContent().substring(0,this.getContent().length() / 2) + "...";
    }

    public String getPicture() {
        return picture;
    }
    @Column(columnDefinition = "text", nullable = false)
    public void setPicture(String picture) {
        this.picture = picture;
    }
}
